﻿using Soleil.Map;
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

        MessageWindow messageW;
        public MessageWindowEvent(Vector _pos, WindowTag _tag, string _message)
            :base(_pos, MessageWindow.GetProperSize(FontID.Test,_message),_tag)
        {
            message = _message;
        }

        public override void Start()
        {
            base.Start();
            messageW = new MessageWindow(Pos, Size, Tag, Wm);
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
