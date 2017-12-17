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

namespace WebApplication1.Controllers
{
    public class UserAsyncController : BaseController
    {
        public UserAsyncController() : base("/UserAsync")
        {
            //get all users
            Get["/", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                return Response.AsJson(res);
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
                Nancy.Json.JavaScriptSerializer js = new Nancy.Json.JavaScriptSerializer();
                return js.Serialize(res1);
            };

            //return "Received POST request";
            //var id = this.Request.Body;
            //var length = this.Request.Body.Length;
            //var data = new byte[length];
            //id.Read(data, 0, (int)length);
            //var body = System.Text.Encoding.Default.GetString(data);

            //var request = JsonConvert.DeserializeObject<SimpleRequest>(body);

            //return 200;

        }
    }
}