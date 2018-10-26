using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum ListenerType
    {
        ItemMenu,
        size,
    }
    /// <summary>
    /// 更新通知を受け取るインターフェイス
    /// </summary>
    interface IListener
    {
        void OnListen(INotifier i);
        ListenerType Type { get; }
    }

    /// <summary>
    /// 更新通知を与えるインターフェイス
    /// </summary>
    interface INotifier { }
}
