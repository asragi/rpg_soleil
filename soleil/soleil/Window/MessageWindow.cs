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
        int drawCharPeriod = 5; // nフレームごとに文字更新
        int charIndex;
        Vector textPos;

        public MessageWindow(Vector _pos, Vector _size, WindowManager wm)
            : base(_pos,_size,wm)
        {
            messageToDraw = "";
            charIndex = 0;
            textPos = pos + new Vector(Spacing, Spacing);
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
            messageToDraw += messageArray[charIndex];
            charIndex++;
        }

        public override void DrawContent(Drawing d)
        {
            d.DrawStaticText(textPos, Resources.GetFont(FontID.Test), messageToDraw, Microsoft.Xna.Framework.Color.White, DepthID.Frame,Vector.One,0,false);
            base.DrawContent(d);
        }
    }
}
