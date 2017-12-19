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
    class MyClass
    {
        [ProtoBuf.ProtoMember(1)]
        public int _nNumber;
        [ProtoBuf.ProtoMember(2)]
        public string _strName;
        [ProtoBuf.ProtoMember(3)]
        public List<ChildClass> _lstInfo;
        [ProtoBuf.ProtoMember(4)]
        public Dictionary<int, string> _dictInfo;
    }

    [ProtoBuf.ProtoContract]
    class ChildClass
    {
        [ProtoBuf.ProtoMember(11)]
        public int id { get; set; }
        [ProtoBuf.ProtoMember(2)]
        public string name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<MyClass> test = new List<MyClass>();
            for (int i=0;i<10000;i++)
            {
                MyClass my = new MyClass();
                my._nNumber = i;
                my._strName = "test";
                my._lstInfo = new List<ChildClass>();
                my._lstInfo.Add(new ChildClass() { id = 1, name = "tww1" });
                my._lstInfo.Add(new ChildClass() { id = 2, name = "tww2" });
                my._lstInfo.Add(new ChildClass() { id = 3, name = "tww3" });
                my._dictInfo = new Dictionary<int, string>();
                my._dictInfo.Add(1, "a");
                my._dictInfo.Add(2, "b");
                my._dictInfo.Add(3, "c");
                test.Add(my);
            }
            var protoBufSerializer = new JSONCompare.ProtoBufSerializer();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var res = protoBufSerializer.Serialize(test);
            sw.Stop();
            Console.WriteLine("protoBufSerializer: Serializing took {0}ms.", sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            List<MyClass> res1 = protoBufSerializer.Deserialize<List<MyClass>>(res);
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
