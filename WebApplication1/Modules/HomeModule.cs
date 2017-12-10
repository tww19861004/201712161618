using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyWebTest.Module
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
    public class HomeModule : BaseModule
    {
        public HomeModule()
        {
            //首页
            Get["/nancy/Home"] = parameters => ReturnHomeAction();
            //返回字符串演示
            Get["/nancy/getStringValue"] = parameters => ReturnStringAction();
            //重定向页面演示
            Get["/nancy/redirectOtherPage"] = parameters => ReturnRedirectAction();
        }
        public dynamic ReturnHomeAction()
        {
            //单一数值
            DynamicModel.HelloWorld = "Hello world...";
            //集合数据 1
            List<string> list1 = new List<string>() { "listValue_1", "listValue_2", "listValue_3", "listValue_4" };
            //集合数据 2 
            List<TestClass> list2 = new List<TestClass>() {
                new TestClass("1","张三"),
                new TestClass("2","李四"),
                new TestClass("3","王五")
            };
            DynamicModel.List1 = list1;
            DynamicModel.List2 = list2;
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
                return Response.AsRedirect("/Login");
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