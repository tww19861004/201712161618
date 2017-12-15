using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tww.MinPrice.Models;
using LightHelper.PetapocoHelper;
using Tww.MinPrice.Services;

namespace NancyWebTest.Controllers
{
    public class TestClass
    {
        public TestClass()
        { }
        public TestClass(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class HomeController : BaseController
    {
        public HomeController()
        {
            //首页
            Get["/Home"] = parameters => ReturnHomeAction();
            //返回字符串演示
            Get["/getStringValue"] = parameters => ReturnStringAction();
            //重定向页面演示
            Get["/redirectOtherPage"] = parameters => ReturnRedirectAction();
            //模拟异步
            Get["/getstringAsync/{id}", runAsync: true] = async (parameters, cancellationToken) =>
            {
                //Request.Query.id
                cancellationToken.ThrowIfCancellationRequested();
                return await Task.FromResult(parameters.id+",Hello World!" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            };
            Get["/getallusers"] = parameters =>
            {                
                //Request.Query.id      
                return Response.AsJson<List<User>>(UserService.GetAllUsers());
                //return await Response.AsJson<List<User>>(UserService.GetAllUsers().Result);
            };
        }

        private async Task<string> GetHelloWorld(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult("Hello World!"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public dynamic ReturnHomeAction()
        {
            //单一数值
            DynamicModel.HelloWorld = "Hello world...";
            //集合数据 1
            List<string> list1 = new List<string>() { "listValue_1", "listValue_2", "listValue_3", "listValue_4" };
            //获取所有的用户 
            
            DynamicModel.List1 = list1;
            //DynamicModel.List2 = list2;
            return View["Home", DynamicModel];
        }
        public dynamic ReturnStringAction()
        {
            List<TestClass> list2 = new List<TestClass>() {
                new TestClass("1","张三"),
                new TestClass("2","李四"),
                new TestClass("3","王五") };

            return Response.AsJson<List<TestClass>>(list2);
            //return "这里一般是一个json串，常用于ajax异步处理，返回json串后页面解析操作等";
        }
        public dynamic ReturnRedirectAction()
        {
            //重定向跳转 1==1 模拟判断用户登录状态有效
            if (1 == 1)
            {
                //当前用户登录状态有效
                return View["Login/Login"];
                //return Response.AsRedirect("Login/Login");
            }
            else
            {
                //跳转到登录页面
                return Response.AsRedirect("/Login");
            }
            return null;
        }
    }
}