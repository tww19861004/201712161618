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

            Get["/{id}", runAsync: true] = async (x, ct) =>
            {
                var res = await UserService.GetAllUsersAsync(ct);
                return Response.AsProtoBuf(res);               
            };

            Get["/get/{id}"] = _ =>
            {
                using (FileStream stream = File.OpenRead(@"D:\MyConfiguration\tww24098\Downloads\"+_.id))
                {
                    //从文件中读取数据并反序列化
                    var res111 = ProtoBuf.Serializer.Deserialize<List<User>>(stream);
                }
                return null;
            };

            Get["/chunked"] = _ =>
            {
                var response = new Response();
                response.ContentType = "text/plain";
                response.Contents = s =>
                {
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes("Hello World ");
                    for (int i = 0; i < 10; ++i)
                    {
                        for (var j = 0; j < 86; j++)
                        {
                            s.Write(bytes, 0, bytes.Length);
                        }
                        s.WriteByte(10);
                        s.Flush();
                        System.Threading.Thread.Sleep(500);
                    }
                };

                return response;
            };
        }
    }
}