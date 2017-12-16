using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Tww.MinPrice.Services;
using Tww.MinPrice.Models;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Extensions;

namespace WebApplication1.Controllers
{
    public class UserController: NancyWebTest.Controllers.BaseController
    //public class UserController : NancyModule
    {
        public UserController()
        {
            Get["/User"] = _ =>
            {
                var response = new Response
                {
                    ContentType = "application/json; charset=utf-8",
                    StatusCode = HttpStatusCode.OK,                    
                };                
                return Response.AsJson<List<User>>(UserService.GetAllUsers());                
            };
            Get["/UserAsync", runAsync: true] = async (_, cancellationToken) =>
            {                
                cancellationToken.ThrowIfCancellationRequested();
                return await Task.FromResult<List<User>>(UserService.GetAllUsers());
            };

            Post["/User/Add"] = _ =>
            {
                //{"Id":1,"Name":"1234","Phone":"15062437243","Email":"382233701@qq.com","Password":"234","CreateTime":"2017-12-11"}                               
                var models = this.Bind<User>();
                return models;
            };

            Post["/User/AddAsync", true] = async (parameters, cancellationToken) =>
            {
                //Content-Type:application/json
                //{"Id":1,"Name":"1234","Phone":"15062437243","Email":"382233701@qq.com","Password":"234","CreateTime":"2017-12-11"}
                cancellationToken.ThrowIfCancellationRequested();
                var models = this.Bind<User>();
                return await Task.FromResult<User>(models);
                //return await Task.FromResult(parameters.id + ",Hello World!" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            };
        }
    }
}