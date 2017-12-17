using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using System.IO;
using Tww.MinPrice.Models;
using System.Threading;

namespace Tww.MinPrice.Services
{
    public  class UserService
    {
        public readonly static string ConnectionString = @"Data Source=" + Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db") + ";Version=3;";
        //插入等操作建议用 using
        public readonly static IDatabase DataBase = new Database(ConnectionString, NPoco.DatabaseType.SQLite);
        public static List<User> GetAllUsers()
        {
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            // create a database "context" object t
            String connectionString = @"Data Source=" + fileName + ";Version=3;";            
            //using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                return DataBase.Query<User>().ToList();
            }
            return null;
        }

        public static async Task<List<User>> GetAllUsersAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            //String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            //String connectionString = @"Data Source=" + fileName + ";Version=3;";
            //并发量比较大的时候推荐用 using
            //using (IDatabase db = new Database(ConnectionString, NPoco.DatabaseType.SQLite))
            {
                //return Task<List<User>>.Run(() => { return db.Query<User>().ToList(); });
                var res = await DataBase.Query<User>().ToListAsync();
                return res;
            };
        }

        public static async Task<User> GetUserByIdAsync(CancellationToken ct,int id)
        {
            using (IDatabase db = new Database(ConnectionString, NPoco.DatabaseType.SQLite))
            {
                //var users = await Database.Query<User>().Where(x => x.UserId == 1).ToListAsync();
                var user = await db.Query<User>().Where(x => x.Id == id).SingleAsync();
                return user;
            }                    
        }

        public static async Task AddAsync(CancellationToken ct, User newUser)
        {
            ct.ThrowIfCancellationRequested();
            if (newUser.Id > 0)
                throw new NotImplementedException();
            using (IDatabase DatabaseNew = new Database(ConnectionString, NPoco.DatabaseType.SQLite))
            {
                await DatabaseNew.InsertAsync(newUser);
            }
        }
        
    }
}
