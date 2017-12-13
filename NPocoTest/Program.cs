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
            String connectionString = "connStringName";

            using (IDatabase db = new Database(connectionString))
            {
                List<User> users = db.Fetch<User>("select * from users");
            }
        }
    }
}
