using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHelper.CommonHelper
{
    public static class Ext
    {        
        public static bool Is(this object value)
        {
            return value != null;
        }

        public static bool Is<T>(this T? value)
            where T : struct
        {
            return value.HasValue;
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNull<T>(this T? value)
            where T : struct
        {
            return !value.HasValue;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> value)
        {
            return value.Is() && value.Any();
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return !value.IsNotEmpty();
        }
    }
}
