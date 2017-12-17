using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}