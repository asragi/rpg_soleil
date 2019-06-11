using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    class ConversationPersonSet: EventBase
    {
        List<ConversationPerson> people;
        ConversationSystem cs;
        public ConversationPersonSet(List<ConversationPerson> list, ConversationSystem system)
        {
            people = list;
            cs = system;
        }

        public override void Execute()
        {
            base.Execute();
            cs.PersonList = people;
            Next();
        }
    }
}
