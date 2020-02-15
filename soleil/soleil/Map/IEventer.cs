using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// マップ上のオブジェクトでEventを持つもの
    /// </summary>
    interface IEventer
    {
        void EventUpdate();
    }
}
