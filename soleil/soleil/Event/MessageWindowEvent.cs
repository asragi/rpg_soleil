using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// メッセージウィンドウを生成する.
    /// </summary>
    class MessageWindowEvent
        :WindowEventBase
    {
        string message;
        public MessageWindowEvent(Vector _pos, Vector _size, WindowTag _tag, string _message, EventManager e)
            :base(_pos,_size,_tag,e)
        {
            message = _message;
        }

        public override void Execute()
        {
            var window = new MessageWindow(Pos, Size, Tag, Wm);
            window.SetMessage(message);
            Next();
        }

    }
}
