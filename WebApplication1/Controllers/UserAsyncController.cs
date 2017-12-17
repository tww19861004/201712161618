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
        }
    }
}