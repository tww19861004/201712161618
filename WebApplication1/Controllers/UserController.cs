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
    public class UserController : Nancy.NancyModule
    //public class UserController : NancyModule
    {
        public UserController() : base("/User")
        {
            //get all users
            Get["/"] = _ => Response.AsJson<object>(GetAll());

            Get["/adfasdf"] = _ =>
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

            // http://localhost:8088/Badges/99
            //Get["/{id}"] = parameter => { return GetById(parameter.id); };

            // http://localhost:8088/Badges       POST: Badge JSON in body
            //Post["/"] = parameter => { return this.AddBadge(); };

            // http://localhost:8088/Badges/99    PUT: Badge JSON in body
            //Put["/{id}"] = parameter => { return this.UpdateBadge(parameter.id); };

            // http://localhost:8088/Badges/99    DELETE:  
            //Delete["/{id}"] = parameter => { return this.DeleteBadge(parameter.id); };
        }

        private object GetAll()
        {
            try
            {
                return UserService.GetAllUsers();                
            }
            catch (Exception e)
            {
                return HandleException(e, String.Format("UserModule.GetAll()"));
            }
        }

        Nancy.Response HandleException(Exception e, String operation)
        {
            // we were trying this operation
            String errorContext = String.Format("{1}:{2}: {3} Exception caught in: {0}", operation, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToShortTimeString(), e.GetType());
            // write detail to the server log. 
            Console.WriteLine("----------------------\n{0}\n{1}\n--------------------", errorContext, e.Message);
            if (e.InnerException != null)
                Console.WriteLine("{0}\n--------------------", e.InnerException.Message);
            // but don't be tempted to return detail to the caller as it is a breach of security.
            return ErrorBuilder.ErrorResponse(this.Request.Url.ToString(), "GET", HttpStatusCode.InternalServerError, "Operational difficulties");
        }

    }
}