using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace JSONCompare
{
    public class BinarySerializer
    {
        private static readonly BinaryFormatter Formatter = new BinaryFormatter();

        public byte[] Serialize(object toSerialize)
        {
            using (var stream = new MemoryStream())
            {
                Formatter.Serialize(stream, toSerialize);
                return stream.ToArray();
            }
        }

        public T Deserialize<T>(byte[] serialized)
        {
            using (var stream = new MemoryStream(serialized))
            {
                var result = (T)Formatter.Deserialize(stream);
                return result;
            }
        }
    }

    public class ProtoBufSerializer
    {
        public byte[] Serialize(object toSerialize)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, toSerialize);
                return stream.ToArray();
            }
        }

        public T Deserialize<T>(byte[] serialized)
        {
            using (var stream = new MemoryStream(serialized))
            {
                var result = ProtoBuf.Serializer.Deserialize<T>(stream);
                return result;
            }
        }
    }

    public class NetSerializerHelper
    {        
        public byte[] Serialize(object toSerialize)
        {
            using (var stream = new MemoryStream())
            {
                
                return stream.ToArray();
            }
        }

        public T Deserialize<T>(byte[] serialized)
        {
            using (var stream = new MemoryStream(serialized))
            {
                var result = ProtoBuf.Serializer.Deserialize<T>(stream);
                return result;
            }
        }
    }
    [ProtoBuf.ProtoContract]
    public partial class User
    {        
        [ProtoBuf.ProtoMember(1)]
        public Int64 Id { get; set; }        
        [ProtoBuf.ProtoMember(2)]
        public String Name { get; set; }        
        [ProtoBuf.ProtoMember(3)]
        public String Phone { get; set; }        
        [ProtoBuf.ProtoMember(4)]
        public String Email { get; set; }        
        [ProtoBuf.ProtoMember(5)]
        public String Password { get; set; }        
        [ProtoBuf.ProtoMember(6)]
        public string CreateTime { get; set; }        
        [ProtoBuf.ProtoMember(7)]
        public int Active { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<User> test = new List<User>();
            for (int i=0;i<10000;i++)
            {
                test.Add(new User()
                { 
                     Id = i,Name = "tww"+i.ToString(), Active = 1, CreateTime = DateTime.Now.ToString("yyyy-MM-dd"), Email = "382233701@qq.com", Password = "Password1234567"
                     , Phone = "15062437264"
                });
            }

            using (FileStream stream = File.OpenWrite("20712201645.dat"))
            {
                //序列化后的数据存入文件
                ProtoBuf.Serializer.Serialize<List<User>>(stream, test);
            }
            using (FileStream stream = File.OpenRead("20712201645.dat"))
            {
                //从文件中读取数据并反序列化
                var test1234 = ProtoBuf.Serializer.Deserialize<List<User>>(stream);
            }

            var protoBufSerializer = new JSONCompare.ProtoBufSerializer();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var res = protoBufSerializer.Serialize(test);
            sw.Stop();
            Console.WriteLine("protoBufSerializer: Serializing took {0}ms.", sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            List<User> res1 = protoBufSerializer.Deserialize<List<User>>(res);
            sw.Stop();
            Console.WriteLine("protoBufSerializer: Deserializing took {0}ms.", sw.Elapsed.TotalMilliseconds);            

            //var NetSerializer = new NetSerializer();
            //sw.Reset();
            //sw.Start();
            //var res2 = NetSerializer.Serialize(test);
            //sw.Stop();
            //Console.WriteLine("NetSerializer: Serializing took {0}ms.", sw.Elapsed.TotalMilliseconds);
            //sw.Reset();
            //sw.Start();
            //var res3 = NetSerializer.Deserialize<List<MyClass>>(res2);
            //sw.Stop();
            //Console.WriteLine("NetSerializer: Deserializing took {0}ms.", sw.Elapsed.TotalMilliseconds);
            Console.ReadKey();
            //测试用数据            
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //using (FileStream stream = File.OpenWrite("test.dat"))
            //{
            //    //序列化后的数据存入文件
            //    ProtoBuf.Serializer.Serialize<List<MyClass>>(stream, test);
            //}
            //sw.Stop();
            //Console.WriteLine("1.protobuf-net序列化成二进制耗时:" + sw.Elapsed.TotalMilliseconds+"毫秒");
            //List<MyClass> my2 = null;
            //sw.Reset();
            //sw.Start();
            
            //sw.Stop();
            //Console.WriteLine("1.protobuf-net二进制反序列化耗时:" + sw.Elapsed.TotalMilliseconds + "毫秒");
            //#region newtonsoft
            //sw.Reset();
            //sw.Start();
            //string str = Newtonsoft.Json.JsonConvert.SerializeObject(test);
            //sw.Stop();
            //Console.WriteLine("2.Newtonsoft序列化耗时:" + sw.Elapsed.TotalMilliseconds + "毫秒");
            //sw.Reset();
            //sw.Start();
            //List<MyClass> res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyClass>>(str);
            //sw.Stop();
            //Console.WriteLine("2.Newtonsoft反序列化耗时:" + sw.Elapsed.TotalMilliseconds + "毫秒");
            //#endregion
            //#region JIL Json
            //sw.Reset();
            //sw.Start();
            //string str1 = Jil.JSON.Serialize(test);
            //sw.Stop();
            //Console.WriteLine("3.JIL JSON序列化耗时:" + sw.Elapsed.TotalMilliseconds + "毫秒");
            //sw.Reset();
            //sw.Start();
            //List<MyClass> res1 = Jil.JSON.Deserialize<List<MyClass>>(str1);
            //sw.Stop();
            //Console.WriteLine("3.JIL JSON反序列化耗时:" + sw.Elapsed.TotalMilliseconds + "毫秒");
            //#endregion
            Console.ReadKey();
        }
    }
}
