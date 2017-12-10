using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jil;

namespace LightHelper.JilJSONHelper
{
    public static partial class JilJSONHelper
    {
        public static string SerializeObject(object value, bool IgnoreNullValue = false, bool Indented = false)
        {
            if (IgnoreNullValue)
            {
                throw new NotImplementedException();
            }
            else
            {
                if (Indented)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    return JSON.Serialize(value);
                }
            }
        }

        public static T DeserializeObject<T>(string value) where T : class
        {
            return JSON.Deserialize<T>(value);
        }
    }
}
