using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 値型の参照
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Reference<T>
    {
        public T Val { get; set; }
        public Reference() { }
        public Reference(T val) => Val = val;
    }
}
