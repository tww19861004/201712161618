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
    public class UserService
    {
        public static List<User> GetAllUsers()
        {
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            // create a database "context" object t
            String connectionString = @"Data Source=" + fileName + ";Version=3;";            
            using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                return db.Query<User>().ToList();
            }
            return null;
        }

        public static async Task<List<User>> GetAllUsersAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            String connectionString = @"Data Source=" + fileName + ";Version=3;";
            using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                //return Task<List<User>>.Run(() => { return db.Query<User>().ToList(); });
                var res = await db.Query<User>().ToListAsync();
                return res;
            };
        }

        public static async Task<User> GetUserByIdAsync(CancellationToken ct,int id)
        {
            return null;
            //ct.ThrowIfCancellationRequested();
            //return await Task.Run<User>(() =>
            //{
            //    String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            //    String connectionString = @"Data Source=" + fileName + ";Version=3;";
            //    using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            //    {
            //        return Task<List<User>>.Run(() => { return db.Query<User>().ToList(); });
            //    }
            //});
        }
    }
}
