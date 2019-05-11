using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// メッセージウィンドウを終了する.
    /// </summary>
    class ConversationEndEvent : EventBase
    {
        ConversationWindow window;
        public ConversationEndEvent(ConversationSystem cs)
        {
            window = cs.ConversationWindow;
        }

        public override void Execute()
        {
            window.Quit();
            Next();
        }
    }
}
