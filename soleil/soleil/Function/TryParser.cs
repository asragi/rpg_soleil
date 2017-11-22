using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCP
{
    static class TryParser
    {
        public static int IntParse(string s)
        {
            int tmp;
            int.TryParse(s, out tmp);
            return tmp;
        }
        public static double DoubleParse(string s)
        {
            double tmp;
            double.TryParse(s, out tmp);
            return tmp;
        }
    }

    static class TryGetter
    {
        public static S TryGet<T,S>(Dictionary<T,S> dict, T key)
        {
            S tmp;
            dict.TryGetValue(key, out tmp);
            return tmp;
        }
    }
}
