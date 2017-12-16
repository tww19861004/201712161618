﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Tww.MinPrice.Services;
using Tww.MinPrice.Models;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Extensions;
using NancyWebTest.Controllers;

namespace WebApplication1.Controllers
{
    public class UserController : BaseController
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
    }
}