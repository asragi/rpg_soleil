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
        private static readonly Vector CharacterMessagePos = new Vector(-50, -200);
        string message;

        MessageWindow messageW;

        /// <summary>
        /// マップ上のオブジェクトを指定してメッセージウィンドウを出す．
        /// </summary>
        public MessageWindowEvent(MapObject obj, string message)
            : this(obj.GetPosition() + CharacterMessagePos, WindowTag.A, message) { }

        /// <summary>
        /// マップ上の座標を指定してメッセージウィンドウを出す．
        /// </summary>
        public MessageWindowEvent(Vector _pos, WindowTag _tag, string _message)
            :base(_pos, MessageWindow.GetProperSize(MessageWindow.DefaultFont, _message),_tag)
        {
            message = _message;
        }

        public override void Start()
        {
            base.Start();
            messageW = new MessageWindow(PosFunc(), Size, Tag, Wm);
            messageW.SetMessage(message);
            messageW.Call();
            // FocusをWindowに設定
            var mim = MapInputManager.GetInstance();
            mim.SetFocus(InputFocus.Window);
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
