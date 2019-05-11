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
    class ConversationEvent: WindowEventBase
    {
        string message;
        ConversationWindow window;
        public ConversationEvent(string _message)
            :base(Vector.Zero, Vector.Zero, 0)
        {
            message = _message;
            window = new ConversationWindow(Tag, Wm);
        }

        public override void Start()
        {
            base.Start();
            window.Call();
        }

        public override void Execute()
        {
            void ReactToInput()
            {
                if (Wm.GetIsMessageWindowAnimFinished(Tag))
                {
                    window.Quit();
                    Next();
                    return;
                }
                Wm.FinishMessageWindowAnim(Tag);
            }

            base.Execute();
            if (KeyInput.GetKeyPush(Key.A)) ReactToInput();
        }
    }
}
