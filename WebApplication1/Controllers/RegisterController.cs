using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Tww.MinPrice.Models;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace WebApplication1.Controllers
{
    public class RegisterController : NancyModule
    {
        public RegisterController()
        {
            //Nancy只需要一行代码就可以收集来自上面的多种来源数据，包括丰富型的JSON和XML类型请求正文，并且可以将他们转换为指定类型的模型的数据实例。
            Post["/Register"] = _ =>
            {
                //{"UserName":"tww","Email":"382233701@qq.com","Password":"1234"}
                //NewUser registerAttempt = this.Bind<NewUser>(); //model binding!                
                //{"Id":"0","Name":"1234","Phone":"15062437243","Email":"382233701@qq.com","Password":"234","CreateTime":"2017-12-11"}
                var models = this.Bind<User>();
                return models;
            };
        }
    }
}