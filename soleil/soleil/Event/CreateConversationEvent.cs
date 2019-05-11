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
        public static EventSet Create(string place, string convName, ConversationSystem cs)
        {
            (var people, var events) = ConversationRead.ActionFromData(place, convName, cs);
            cs.SetPeople(people);
            var initEvent = new EventBase[] { new ChangeInputFocusEvent(InputFocus.Window), new ConversationInitEvent(cs) };
            var endEvent = new EventBase[] { new ConversationEndEvent(cs), new ChangeInputFocusEvent(InputFocus.Player) };

            // Add Events before and after
            var result = (initEvent.Concat(events)).Concat(endEvent);
            return new EventSet(result.ToArray());
        }
    }
}
