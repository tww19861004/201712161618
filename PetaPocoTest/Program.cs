using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using PetaPoco = LightHelper.PetapocoHelper;
using LightHelper.NewtonJSONHelper;

namespace PetaPocoTest
{
    [PetaPoco.TableName("User")]
    [PetaPoco.PrimaryKey("Id")]
    public partial class User
    {
        [PetaPoco.Column("Id")]
        public Int64 Id { get; set; }
        [PetaPoco.Column("Name")]
        public String Name { get; set; }
        [PetaPoco.Column("Phone")]
        public String Phone { get; set; }
        [PetaPoco.Column("Email")]
        public String Email { get; set; }
        [PetaPoco.Column("Password")]
        public String Password { get; set; }
        [PetaPoco.Column("CreateTime")]
        public string CreateTime { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            User newUser = new User()
            {
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd"),
                Email = "382233701@qq.com",
                Name = "1234",
                Password = "234",
                Phone = "15062437243"
            };
            string str = NewtonJSONHelper.ToJson(newUser);
            return;

            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            // create a database "context" object t
            String connectionString = String.Format("Data Source={0};Version=3", fileName);
            DbProviderFactory sqlFactory = new System.Data.SQLite.SQLiteFactory();
            PetaPoco.Database db = new PetaPoco.Database(connectionString, sqlFactory);

            #region 创建测试表1
            string dropQuery = "DROP TABLE User";
            db.Execute(dropQuery);
            String createQuery =
                        @"CREATE TABLE IF NOT EXISTS
                            [User] (
                            [id]           INTEGER      NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Name]         VARCHAR(20)  NOT NULL DEFAULT 'tww',
                            [Phone]        VARCHAR(20)  NOT NULL,
                            [Email]        VARCHAR(20)  NOT NULL,
                            [Password]     VARCHAR(20)  NOT NULL                            
                            )";
            db.Execute(createQuery);
            #endregion

            #region 非查询命令 delete 清空数据
            db.Execute("DELETE FROM User;VACUUM");
            #endregion

            #region CREATE INDEX 
            string create_index = "CREATE INDEX IF NOT EXISTS idx_id ON User (id);";
            db.Execute(create_index);
            #endregion

            #region  alter table add column
            DateTime d = db.ExecuteScalar<DateTime>("select date('now');");
            //String alterQuery = @"ALTER TABLE User ADD CreateTime TEXT";            
            //db.Execute(alterQuery);
            #endregion

            #region 多任务插入测试数据
            Func<object, object> fun = (object obj) =>
            {
                // The tasks that receive an argument between 2 and 5 throw exceptions
                int i = (int)obj;
                //if (i==11)
                //{
                //    throw new InvalidOperationException("SIMULATED EXCEPTION");
                //}
                PetaPoco.Database db1 = new PetaPoco.Database(connectionString, sqlFactory);
                return db1.Insert("User", "id", new { Name = "user" + i.ToString(), Phone = "123412341234", Email = "12341@qq.com", Password = "1234" });
            };
            int taskNum = 20;
            Task<object>[] tasks = new Task<object>[taskNum];
            for (int i = 0; i < taskNum; i++)
            {
                tasks[i] = Task<object>.Factory.StartNew(fun, i);
            }
            // Exceptions thrown by tasks will be propagated to the main thread
            // while it waits for the tasks. The actual exceptions will be wrapped in AggregateException.
            try
            {
                // Wait for all the tasks to finish.
                Task.WaitAll(tasks);

                // We should never get to this point
                Console.WriteLine("WaitAll() has not thrown exceptions. THIS WAS NOT EXPECTED.");
            }
            catch (AggregateException e)
            {
                Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                }
            }
            finally
            {
                //db.Execute("DELETE FROM User;VACUUM");
            }
            #endregion

            String sql1 = "select * from User order by id desc";
            foreach (User rec in db.Query<User>(sql1))
            {
                Console.WriteLine("{0},{1},{2},{3},{4}", rec.Id, rec.Name, rec.Email, rec.Password, rec.CreateTime);
            }
            Console.ReadKey();
            return;


            String sql = "select * from User order by id desc";
            foreach (User rec in db.Query<User>(sql))
            {
                Console.WriteLine("{0},{1},{2},{3}", rec.Id, rec.Name, rec.Email, rec.Password);
            }

            //查询标量
            long count = db.ExecuteScalar<long>("SELECT Count(1) FROM User");

            //查询单条数据
            var a = db.SingleOrDefault<User>("SELECT * FROM User WHERE id=@0", 2);
            if (a != null)
            {
                a.Phone = "12345";
                db.Update("User", "id", a);
                var a1 = db.SingleOrDefault<User>("SELECT * FROM User WHERE id=@0", 2);
            }

            //匿名更新
            db.Update("User", "id", new { phone = "15195661894" }, 12);
            var a2 = db.SingleOrDefault<User>("SELECT * FROM User WHERE id=@0", 12);

            //使用IsNew可以检测记录是否在数据表中存在：
            //Save方法会自动发送Insert（如果表中不存在）或Update子句。

            //调用存储过程
            //db.Execute("exec procSomeHandler @0, @1", 3, "2011-10-01");

            //调用带输出(OUTPUT)参数的存储过程, 写的sql语句，@0参数后的“output”是关键
            //var param = new SqlParameter() { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
            //db.Execute("exec procSomeHandler @0 OUTPUT", param);


            using (var scope = db.GetTransaction())
            {
                var a21 = db.SingleOrDefault<User>("SELECT * FROM User WHERE id=@0", 12);
                scope.Complete();
            }

            //分页查询
            var result = db.Page<User>(1, 2, // <-- page number and items per page
                "SELECT * FROM User WHERE Phone=@0 ORDER BY id DESC", "123412341234");

            Console.ReadKey();
            //createQuery =
            //            @"CREATE TABLE IF NOT EXISTS
            //                [solution] (
            //                [id]           INTEGER      NOT NULL PRIMARY KEY AUTOINCREMENT,
            //                [parent]       INTEGER      NOT NULL,
            //                [level]        INTEGER      NOT NULL,
            //                [name]         VARCHAR(256) NOT NULL,
            //                [itemtypeid]   INTEGER      NOT NULL,
            //                FOREIGN KEY (itemtypeid) REFERENCES itemtype(id)
            //                )";

            //ExecuteNonQuery(dbConnection, createQuery);           
        }

        // Helper extracted from SqliteHelper.cs
        public static int ExecuteNonQuery(string dbConnection, string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            try
            {
                cnn.Open();
                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                int rowsUpdated = mycommand.ExecuteNonQuery();
                return rowsUpdated;
            }
            catch (Exception fail)
            {
                Console.WriteLine(fail.Message);
                return 0;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
