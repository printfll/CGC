using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CGC.Comm
{
    public static class BondTypeAliasConverter
    {
        public static long Convert(DateTime value, long unused)
        {
            return value.Ticks;
        }

        public static DateTime Convert(long value, DateTime unused)
        {
            return new DateTime(value);
        }

        public static string Convert(Guid value, string unused)
        {
            return value.ToString();
        }

        public static Guid Convert(string value, Guid unused)
        {
            return new Guid(value);
        }
    }
}
