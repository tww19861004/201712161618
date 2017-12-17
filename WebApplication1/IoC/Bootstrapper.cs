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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApplication1
{

    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //this.Formatting = Formatting.Indented;
        }
    }
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CustomJsonSerializer>();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // Enable Compression with Settings
            //var settings = new GzipCompressionSettings();
            //settings.MinimumBytes = 1024;
            //settings.MimeTypes.Add("application/vnd.myexample");
            //pipelines.EnableGzipCompression(settings);

            // Enable Compression with Default Settings            
            pipelines.EnableGzipCompression();            
            base.ApplicationStartup(container, pipelines);
            //JsonSettings.DefaultCharset = "utf-8";
            //Nancy.Json.JsonSettings.MaxJsonLength = 20971520;
            //StaticConfiguration.EnableRequestTracing = true;
            //StaticConfiguration.DisableErrorTraces = false;
            //Csrf.Enable(pipelines);
            //CookieBasedSessions.Enable(pipelines);
        }
    }
}