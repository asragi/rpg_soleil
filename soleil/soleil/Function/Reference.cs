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
        T value;
        public Reference() { }
        public Reference(T _value) => value = _value;
        public T Get() => value;
    }
}
