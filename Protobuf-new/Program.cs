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
            Stopwatch sw = new Stopwatch();           

            using (HttpClient hp1 = new HttpClient())
            {
                hp1.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                string test1 = "http://localhost:12018/userasync/jiljsontest";
                sw.Reset();
                sw.Start();
                var res1 = Jil.JSON.Deserialize<List<User>>(hp1.GetStringAsync(test1).Result);
                sw.Stop();                
                Console.WriteLine("jiljson一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res1.Count.ToString());
            }

            System.Threading.Thread.Sleep(100);

            using (HttpClient hp1 = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression =
    System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }))
            {
                hp1.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                string test1 = "http://localhost:12018/userasync/jiljsontest";
                sw.Reset();
                sw.Start();
                var res1 = Jil.JSON.Deserialize<List<User>>(hp1.GetStringAsync(test1).Result);
                sw.Stop();
                Console.WriteLine("jiljson with gzip一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res1.Count.ToString());
            }
            System.Threading.Thread.Sleep(100); 
            using (HttpClient hp = new HttpClient())
            {
                hp.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-protobuf; charset=utf-8");
                string test = "http://localhost:12018/protobufnet/2019";
                sw.Reset();
                sw.Start();
                var res = ProtoBuf.Serializer.Deserialize<List<User>>(hp.GetStreamAsync(test).Result);
                sw.Stop();
                Console.WriteLine("protobuf一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res.Count.ToString());
            }

            System.Threading.Thread.Sleep(100);

            using (HttpClient hp = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression =
    System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }))
            {
                hp.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-protobuf; charset=utf-8");
                string test = "http://localhost:12018/protobufnet/2019";
                sw.Reset();
                sw.Start();
                var res = ProtoBuf.Serializer.Deserialize<List<User>>(hp.GetStreamAsync(test).Result);
                sw.Stop();
                Console.WriteLine("protobuf with gzip一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res.Count.ToString());
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(100);
            using (HttpClient hp1 = new HttpClient())
            {
                hp1.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                string test1 = "http://localhost:12018//userasync/newtonjsontest";
                sw.Reset();
                sw.Start();
                var res1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(hp1.GetStringAsync(test1).Result);
                sw.Stop();
                Console.WriteLine("newtonsoft一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res1.Count.ToString());
            }

            System.Threading.Thread.Sleep(100);

            using (HttpClient hp1 = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression =
    System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }))
            {
                hp1.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                string test1 = "http://localhost:12018//userasync/newtonjsontest";
                sw.Reset();
                sw.Start();
                var res1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(hp1.GetStringAsync(test1).Result);
                sw.Stop();
                Console.WriteLine("newtonsoft with gzip一共耗时:" + sw.Elapsed.TotalMilliseconds + "，返回：" + res1.Count.ToString());
            }
            Console.WriteLine();
            

            Console.ReadKey();
        }
    }
}
