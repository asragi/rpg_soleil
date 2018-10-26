using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 更新通知を受け取るインターフェイス
    /// </summary>
    interface IListener
    {
        void OnListen();
    }
}
