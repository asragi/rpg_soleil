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
        public MessageWindowEvent(Vector _pos, WindowTag _tag, string _message)
            :base(_pos, MessageWindow.GetProperSize(FontID.Test,_message),_tag)
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
