using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Json;
using Nancy;
using Nancy.Session;
using Nancy.Security;
using System.IO;

namespace WebApplication1
{
    //1、创建自己的jsonserializer类
    //public class CustomJsonNetSerializer : JsonSerializer, ISerializer
    //{
    //    public CustomJsonNetSerializer()
    //    {
    //        ContractResolver = new CamelCasePropertyNamesContractResolver();
    //        DateFormatHandling = DateFormatHandling.IsoDateFormat;
    //        Formatting = Formatting.None;
    //    }

    //    public bool CanSerialize(string contentType)
    //    {
    //        return contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
    //    }

    //    public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
    //    {
    //        using (var streamWriter = new StreamWriter(outputStream))
    //        using (var jsonWriter = new JsonTextWriter(streamWriter))
    //        {
    //            Serialize(jsonWriter, model);
    //        }
    //    }

    //    public IEnumerable<string> Extensions { get { yield return "json"; } }
    //}
    public class ApplicationBootrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            JsonSettings.DefaultCharset = "utf-8";
            Nancy.Json.JsonSettings.MaxJsonLength = 20971520;
            StaticConfiguration.EnableRequestTracing = true;
            StaticConfiguration.DisableErrorTraces = false;
            Csrf.Enable(pipelines);
            CookieBasedSessions.Enable(pipelines);
        }
    }
}