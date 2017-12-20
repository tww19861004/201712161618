using NPoco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tww.MinPrice.Models;

namespace NPocoTest
{
    class Program
    {
        static void Main(string[] args)
        {            
            //var list = await QueryAsync();            
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            String connectionString = @"Data Source=" + fileName + ";Version=3;";
            IDatabase Database = new Database(connectionString, NPoco.DatabaseType.SQLite);
            // create a database "context" object t                        

            //读取
            //Parallel.For(0, 666, (i) =>
            //{
            //    using (IDatabase Database1 = new Database(connectionString, NPoco.DatabaseType.SQLite))
            //    {
            //        Database1.Query<User>().Where(x => x.Id == i).SingleAsync();
            //    }
            //});

            //写入
            Parallel.For(0,1500, (i) =>
            {
                using (IDatabase Database1 = new Database(connectionString, NPoco.DatabaseType.SQLite))
                {
                    User user = new User();
                    //u.Id = i;
                    user.Name = "tww" + i.ToString();
                    user.Phone = "15062437243";
                    user.Email = "382233701@qq.com";
                    user.Password = "1234";
                    user.Active = 1;
                    user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    Database1.InsertAsync(user);
                }                   
            });

            //var pk = await Database.InsertAsync(user);

            //var userDb = Database.Query<User>().Where(x => x.UserId == user.UserId).Single();

            //using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                Task<List<User>>.Run(() => { return Database.Query<User>().Where(x => x.Id == 1).ToListAsync(); });
            }
        }

        //public static async Task<List<User>> QueryAsync()
        //{
        //    return await Task.Run<List<User>>(()=>
        //    {
        //        String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
        //        // create a database "context" object t
        //        String connectionString = @"Data Source=" + fileName + ";Version=3;";

        //        using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
        //        {
        //            return Task<List<User>>.Run(() => { return db.Query<User>().Where(x => x.Id == 1).ToListAsync(); });
        //        }                
        //    });           
        //}
    }
}
