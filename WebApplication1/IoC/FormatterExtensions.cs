using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public static class FormatterExtensions
    {
        public static Response AsNewtonJson<TModel>(this IResponseFormatter formatter, TModel model)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(model, Formatting.None);
        }

        public static Response AsJilJson<TModel>(this IResponseFormatter formatter, TModel model)
        {
            return Jil.JSON.Serialize(model);
        }

        public static Response AsProtoBuf<TModel>(this IResponseFormatter formatter, TModel model)
        {
            var response = new Response();
            response.WithContentType("application/x-protobuf");            
            response.Contents = s =>
            {
                using (var stream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(stream, model);
                    byte[] b = stream.ToArray();
                    s.Write(b, 0, b.Length);
                    stream.Flush();
                    s.Flush();
                }
            };
            return response;
        }
    }
}