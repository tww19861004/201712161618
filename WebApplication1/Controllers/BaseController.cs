﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Dynamic;

namespace NancyWebTest.Controllers
{
    public class BaseController : NancyModule
    {
        //声明动态对象，用于控制器绑定数据传递到页面
        public dynamic DynamicModel = new ExpandoObject();

        public BaseController()
        {
            SetupModelDefaults();
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
    }
}