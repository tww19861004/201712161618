using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Tww.MinPrice.Models;
using Nancy.ModelBinding;
using Nancy.Extensions;

namespace WebApplication1.Controllers
{
    public class LoginController : NancyModule
    {

        public LoginController()
        {
            //http://localhost:12018/Login/123/31
            Get["/Login/{uname}/{pwd}"] = p =>
            {
                if (p.uname == "tww" && p.pwd == "1234")
                {
                    Guid token = Guid.NewGuid();
                    //todo: 发送给网关服务器
                    //...
                    return Response.AsJson(new { Result = "OK", Token = token });
                }
                return Response.AsJson(new { Result = "falied"});
            };

            //http://localhost:12018/Login?&name=123&age=31
            Get["/Login"] = x => { return "person name :" + Request.Query.name + " age : " + Request.Query.age; };

            // would capture routes like /users/192/add/moderator sent as a POST request
            Post["/Login/{uname}/{pwd}"] = parameters => {
                User newUser = new User()
                {
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    Email = "382233701@qq.com",
                    Name = parameters.uname,
                    Password = parameters.pwd,
                    Phone = "15062437243"
                };                
                //Customer model = this.Bind();
                //var models = this.Bind<Customer>();
                //DB.Customer.Add(model);
                //DB.Customer.Add(models);
                //return this.Response.AsRedirect("/Customers");
                return HttpStatusCode.OK;
            };

            Post["/Login"] = _ =>
            {
                var body = this.Request.Body;
                int length = (int)body.Length; // this is a dynamic variable
                byte[] data = new byte[length];
                body.Read(data, 0, length);
                string str = System.Text.Encoding.Default.GetString(data);

                User body1 = this.Bind<User>();
                return null;
            };

            //重定向页面演示
            Get["/nancy/Login"] = parameters =>
            {
                return View["Login/Login"];
                //return Response.AsRedirect("Login/Login");
            };
        }
    }
}