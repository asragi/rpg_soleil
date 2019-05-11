using Soleil.Event.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Conversation
{
    class ConversationTalk: WindowEventBase
    {
        ConversationWindow window;
        string message;
        ConversationPerson person;
        public ConversationTalk(ConversationPerson _person, string _message, ConversationSystem _cs)
            :base(Vector.Zero, Vector.Zero, WindowTag.Conversation)
        {
            (person, message, window) = (_person, _message, _cs.ConversationWindow);
        }

        public override void Start()
        {
            base.Start();
            window.SetMessage(message);
        }

        public override void Execute()
        {
            base.Execute();
            if (KeyInput.GetKeyPush(Key.A)) ReactToInput();

            void ReactToInput()
            {
                if (Wm.GetIsMessageWindowAnimFinished(Tag))
                {
                    Next();
                    return;
                }
                Wm.FinishMessageWindowAnim(Tag);
            }
        }
    }
}
