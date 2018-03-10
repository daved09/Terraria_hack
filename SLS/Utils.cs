using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    public static class Utils
    {
        public static T ConvertTo<T>(this object str)
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }

        public static bool CanConvert<T>(this object str)
        {
            try
            {
                str.ConvertTo<T>();
                return true;
            }
            catch { }

            return false;
        }

        public static bool TryConvert<T>(this object str, out T value)
        {
            value = default(T);
            if (!str.CanConvert<T>())
                return false;

            value = str.ConvertTo<T>();

            return true;
        }
    }
}
