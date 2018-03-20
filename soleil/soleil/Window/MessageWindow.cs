using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class MessageWindow
        :SelectableWindow
    {
        String message;
        char[] messageArray;
        String messageToDraw;
        int drawCharPeriod = 8; // nフレームごとに文字更新
        int charIndex;

        public MessageWindow(Vector _pos, Vector _size, WindowManager wm)
            : base(_pos,_size,wm)
        {
            messageToDraw = "";
            charIndex = 0;
        }

        public void SetMessage(String msg)
        {
            message = msg;
            messageArray = message.ToCharArray();
            messageToDraw = ""; // 表示メッセージを初期化
            charIndex = 0;
        }

        public override void Update()
        {
            if (frame % drawCharPeriod == 0) AddChar();
            base.Update();

        }

        void AddChar()
        {
            if (messageArray == null) return;
            if (charIndex >= messageArray.Length) return;
            Console.WriteLine(messageArray.Length+":"+charIndex);
            messageToDraw += messageArray[charIndex];
            charIndex++;
        }

        public override void DrawContent(Drawing d)
        {
            d.DrawText(pos, Resources.GetFont(FontID.Test), messageToDraw, Microsoft.Xna.Framework.Color.White, DepthID.Frame);
            base.DrawContent(d);
        }
    }
}
