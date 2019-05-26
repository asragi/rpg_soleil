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
    class ConversationInitEvent: EventBase
    {
        ConversationWindow window;
        public ConversationInitEvent(ConversationSystem cs)
        {
            window = cs.ConversationWindow;
        }

        public override void Execute()
        {
            window.Call();
            Next();
        }
    }
}
