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
    class ConversationEvent: EventBase
    {
        string message;
        ConversationWindow window;
        public ConversationEvent(string _message)
        {
            message = _message;
            window = new ConversationWindow();
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
                window.Quit();
                Next();
            }

            base.Execute();
            if (KeyInput.GetKeyPush(Key.A)) ReactToInput();
        }
    }
}
