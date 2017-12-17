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
using NancyWebTest.Controllers;
using System.Threading;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using Nancy.Responses;

namespace WebApplication1.Controllers
{
    public class UserAsyncController : BaseController
    {
        public UserAsyncController() : base("/userasync")
        {
            //get all users
            Get["/", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                return Response.AsNewtonJson(res);
            };
            Get["/jil", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                return Jil.JSON.Serialize(res);
            };
            #region NegotiatorExtensions.test
            Get["/test"] = _ =>
            {
                var res = UserService.GetAllUsers().First();
                return NegotiatorExtensions.WithModel(Negotiate.WithStatusCode(HttpStatusCode.OK), res);
            };
            #endregion
            //get the user by id
            Get["/{id:int}", runAsync: true] = async (x, ct) =>
            {
                var res1 = await UserService.GetUserByIdAsync(ct,x.id);
                return Jil.JSON.Serialize(res1);
            };

            //{ "Name":"1234","Phone":"13092130556","Email":"382233701@qq.com","Password":"234","CreateTime":"2017-12-11","Active":1}
            Post["/add",true] = async (x, ct) =>
            {
                ct.ThrowIfCancellationRequested();
                //return "Received POST request";
                var id = this.Request.Body;
                var length = this.Request.Body.Length;
                var data = new byte[length];
                id.Read(data, 0, (int)length);
                var body = System.Text.Encoding.Default.GetString(data);
                var newUser = Jil.JSON.Deserialize<User>(body);                
                await UserService.AddAsync(ct,newUser);
                return HttpStatusCode.OK;
            };

            Put["/update", true] = async (x, ct) =>
            {
                ct.ThrowIfCancellationRequested();
                //return "Received POST request";
                //{ "Id":8,"Name":"1234","Phone":"130921310556","Email":"382233701@qq.com","Password":"234","CreateTime":"2017-12-11","Active":1}
                var user = this.GetReqObj<User>();           
                int res = await UserService.UpdateAsync(ct, user);
                if (res > 0)
                    return HttpStatusCode.OK;
                else
                    return HttpStatusCode.InternalServerError;
            };
        }
        
        // POST /Badges        
    }
}