using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Dynamic;
using WebApplication1;

namespace NancyWebTest.Controllers
{
    public class BaseController : Nancy.NancyModule
    {
        //声明动态对象，用于控制器绑定数据传递到页面
        public dynamic DynamicModel = new ExpandoObject();

        public BaseController()
        {
            SetupModelDefaults();
        }

        public BaseController(string modulePath) : base(modulePath)
        {            
        }

        private void SetupModelDefaults()
        {
            Before += ctx =>
            {
                return null;
            };
            OnError += (ctx, ex) => {
                return null;
            };
        }

        protected Nancy.Response HandleException(Exception e, String operation)
        {
            // we were trying this operation
            String errorContext = String.Format("{1}:{2}: {3} Exception caught in: {0}", operation, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToShortTimeString(), e.GetType());
            // write detail to the server log. 
            Console.WriteLine("----------------------\n{0}\n{1}\n--------------------", errorContext, e.Message);
            if (e.InnerException != null)
                Console.WriteLine("{0}\n--------------------", e.InnerException.Message);
            // but don't be tempted to return detail to the caller as it is a breach of security.
            return ErrorBuilder.ErrorResponse(this.Request.Url.ToString(), "GET", HttpStatusCode.InternalServerError, "Operational difficulties");
        }
    }
}