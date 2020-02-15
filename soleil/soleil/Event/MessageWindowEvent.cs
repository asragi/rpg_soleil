using Microsoft.Xna.Framework;
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
        : WindowEventBase
    {
        string message;

        MessageWindow messageW;
        bool isSpeech;
        bool backImg;

        /// <summary>
        /// マップ上のオブジェクトを指定してメッセージウィンドウを出す．
        /// </summary>
        public MessageWindowEvent(MapObject obj, string _message)
            : base(obj, MessageWindow.GetProperSize(MessageWindow.DefaultFont, _message))
        {
            message = _message;
            isSpeech = true;
            backImg = true;
        }

        /// <summary>
        /// マップ上の座標を指定してメッセージウィンドウを出す．
        /// </summary>
        public MessageWindowEvent(Vector _pos, WindowTag _tag, string _message, bool speech = true, bool center = false, bool _backImg = true)
            : base(_pos, MessageWindow.GetProperSize(MessageWindow.DefaultFont, _message), _tag, center)
        {
            isSpeech = speech;
            message = _message;
            backImg = _backImg;
        }

        public override void Start()
        {
            base.Start();
            // Window生成
            messageW = isSpeech ? new SpeechBubbleWindow(PosFunc(), Size)
                : new MessageWindow(PosFunc(), Size);
            // ウィンドウ生成
            messageW.SetMessage(message);
            messageW.Call();
            // ウィンドウ背景がない場合の処理
            if (!backImg)
            {
                messageW.Color = ColorPalette.AliceBlue;
                messageW.BackImgAlpha = 0;
            }
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
