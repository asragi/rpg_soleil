using Soleil.Map;
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
        MapInputManager inputManager;

        MessageWindow messageW;
        public MessageWindowEvent(Vector _pos, WindowTag _tag, string _message, MapInputManager mapInput)
            :base(_pos, MessageWindow.GetProperSize(MessageWindow.DefaultFont, _message),_tag)
        {
            message = _message;
            inputManager = mapInput;
        }

        public override void Start()
        {
            base.Start();
            messageW = new MessageWindow(Pos, Size, Tag, Wm);
            messageW.SetMessage(message);
            messageW.Call();
            // FocusをWindowに設定
            inputManager.SetFocus(InputFocus.Window);
        }

        public override void Execute()
        {
            base.Execute();
            if (KeyInput.GetKeyPush(Key.A))
            {
                ReactToInput();
            }
            void ReactToInput()
            {
                if (Wm.GetIsMessageWindowAnimFinished(Tag))
                {
                    messageW.Quit();
                    Next();
                    return;
                }
                Wm.FinishMessageWindowAnim(Tag);
            }
        }

    }
}
