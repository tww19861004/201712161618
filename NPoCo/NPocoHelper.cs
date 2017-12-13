using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tww.MinPrice.Models;
using NPoco;


namespace NPoCo
{
    public class NPoCoHelper
    {
        public async Task InsertPocoIntoDatabaseUsingInsertAsync()
        {
            using (IDatabase db = new Database("connStringName"))
            {
                List<User> users = db.Fetch<User>("select userId, email from users");                
            }

            User user = new User();
            //u.Id = i;
            user.Name = "tww";
            user.Phone = "15062437243";
            user.Email = "382233701@qq.com";
            user.Password = "1234";
            user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd");

            //var pk = await Database.InsertAsync(user);

            //var userDb = Database.Query<User>().Where(x => x.UserId == user.UserId).Single();
            
        }        
    }
}
