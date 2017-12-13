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
            String fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "tww1.db");
            // create a database "context" object t
            String connectionString = @"Data Source="+ fileName + ";Version=3;";

            using (IDatabase db = new Database(connectionString, NPoco.DatabaseType.SQLite))
            {
                List<User> users = db.Fetch<User>("select * from user");
            }
        }
    }
}
