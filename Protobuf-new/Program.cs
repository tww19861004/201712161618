using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tww.MinPrice.Models;

namespace Protobuf_new
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient hp = new HttpClient();
            string test = "http://localhost:12018/protobufnet/2019";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var res = ProtoBuf.Serializer.Deserialize<List<User>>(hp.GetStreamAsync(test).Result);
            sw.Stop();
            Console.WriteLine("protobuf一共耗时:"+sw.Elapsed.TotalMilliseconds);
            HttpClient hp1 = new HttpClient();
            string test1 = "http://localhost:12018//userasync/newtonjsontest";
            sw.Reset();
            sw.Start();
            var res1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(hp1.GetStringAsync(test1).Result);
            sw.Stop();
            Console.WriteLine("newtonsoft一共耗时:" + sw.Elapsed.TotalMilliseconds);
            Console.ReadKey();
        }
    }
}
