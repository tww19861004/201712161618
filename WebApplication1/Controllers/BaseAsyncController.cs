using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Dynamic;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NancyWebTest.Controllers
{
    public class BaseAsyncController : NancyModule
    {
        //声明动态对象，用于控制器绑定数据传递到页面
        public dynamic DynamicModel = new ExpandoObject();

        public BaseAsyncController()
        {
            SetupModelDefaults();
        }

        private void SetupModelDefaults()
        {
            Before += async (ctx, ct) =>
            {
                this.AddToLog("Before Hook Delay<br/>");
                await Task.Delay(5000);

                return null;
            };

            After += async (ctx, ct) =>
            {
                this.AddToLog("After Hook Delay<br/>");
                await Task.Delay(5000);
                this.AddToLog("After Hook Complete<br/>");

                ctx.Response = this.GetLog();
            };

            Get["/nancy/getAsync", runAsync: true] = async (x, ct) =>
            {
                this.AddToLog("Delay 1<br/>");
                await Task.Delay(1000);

                this.AddToLog("Delay 2<br/>");
                await Task.Delay(1000);

                this.AddToLog("Executing async http client<br/>");
                var client = new HttpClient();
                var res = await client.GetAsync("http://nancyfx.org");
                var content = await res.Content.ReadAsStringAsync();

                this.AddToLog("Response: " + content.Split('\n')[0] + "<br/>");

                return (Response)this.GetLog();
            };
        }

        private void AddToLog(string logLine)
        {
            if (!this.Context.Items.ContainsKey("Log"))
            {
                this.Context.Items["Log"] = string.Empty;
            }

            this.Context.Items["Log"] = (string)this.Context.Items["Log"] + DateTime.Now.ToLongTimeString() + " : " + logLine;
        }

        private string GetLog()
        {
            if (!this.Context.Items.ContainsKey("Log"))
            {
                this.Context.Items["Log"] = string.Empty;
            }

            return (string)this.Context.Items["Log"];
        }
    }
}