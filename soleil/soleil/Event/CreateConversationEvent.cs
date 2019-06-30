using Soleil.Event.Conversation;
using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// 会話シーンの会話内容を場所と会話idから探してEventSetを返す．
    /// </summary>
    static class CreateConversationEvent
    {
        public static EventSet[] Create(string place, string convName, ConversationSystem cs, EventSequence es, MapInputManager mim)
        {
            var events = ConversationRead.ActionFromData(place, convName, cs, es);
            var initEvent = new EventSet(new ChangeInputFocusEvent(InputFocus.Window, mim), new ConversationInitEvent(cs));
            var endEvent = new EventSet(new ConversationEndEvent(cs), new ChangeInputFocusEvent(InputFocus.Player, mim));

            // Add Events before and after
            var result = new EventSet[events.Length + 2];
            result[0] = initEvent;
            for (int i = 0; i < events.Length; i++)
            {
                result[i + 1] = events[i];
            }
            result[events.Length + 1] = endEvent;
            return result;
        }
    }
}
