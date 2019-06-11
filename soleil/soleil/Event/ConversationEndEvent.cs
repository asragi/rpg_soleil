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
        ConversationSystem cs;
        public ConversationEndEvent(ConversationSystem _cs)
        {
            cs = _cs;
        }

        public override void Execute()
        {
            cs.Quit();
            Next();
        }
    }
}
