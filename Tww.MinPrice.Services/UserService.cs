using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using System.IO;
using Tww.MinPrice.Models;

namespace Tww.MinPrice.Services
{
    public class UserService
    {
        public static List<User> GetAllUsers()
        {
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            // create a database "context" object t
            String connectionString = @"Data Source=" + fileName + ";Version=3;";
            throw new InvalidOperationException("simulate the expection state");
            using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                return db.Query<User>().ToList();
            }
            return null;
        }

        public static async Task<List<User>> GetAllUsersAsync()
        {             
            return await Task.Run<List<User>>(() =>
            {
                if (1 == 1)
                {
                    throw new InvalidOperationException("simulate the expection state");
                }
                String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
                // create a database "context" object t
                String connectionString = @"Data Source=" + fileName + ";Version=3;";

                using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
                {
                    return Task<List<User>>.Run(() => { return db.Query<User>().ToList(); });
                }
            });
        }
    }
}
