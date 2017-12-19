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
using System.IO;

namespace WebApplication1.Controllers
{
    public class ProtobufnetController : BaseController
    {
        public ProtobufnetController() : base("/protobufnet")
        {
            Get["/test", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                using (FileStream stream = File.OpenWrite("test.dat"))
                {
                    //序列化后的数据存入文件
                    ProtoBuf.Serializer.Serialize<List<User>>(stream, res);
                }
                using (FileStream stream = File.OpenRead("test.dat"))
                {
                    //从文件中读取数据并反序列化
                    var test = ProtoBuf.Serializer.Deserialize<List<User>>(stream);
                }
                return Response.AsJilJson(res);
            };

            Get["/test.dat", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                var stream = new MemoryStream();
                {
                    ProtoBuf.Serializer.Serialize(stream, res);
                    return Response.FromStream(stream, "application/x-protobuf");
                }
            };

            Get["/2", runAsync: true] = async (x, ct) =>
            {
                using (FileStream stream = File.OpenRead(@"D:\MyConfiguration\tww24098\Downloads\test.dat"))
                {
                    //从文件中读取数据并反序列化
                    var res111 = ProtoBuf.Serializer.Deserialize<List<User>>(stream);
                }
                return null;
            };
        }
    }
}