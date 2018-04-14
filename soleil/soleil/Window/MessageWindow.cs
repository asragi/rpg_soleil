using System;

namespace Soleil
{
    class MessageWindow
        :Window
    {
        String message;
        char[] messageArray;
        String messageToDraw; // 表示されるStringを保持する変数
        int drawCharPeriod = 4; // nフレームごとに文字更新
        int charIndex; // char配列アクセス用index
        Vector textPos;
        bool endAnimation;

        public MessageWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm)
            : base(_pos,_size,tag,wm)
        {
            endAnimation = false;
            messageToDraw = "";
            charIndex = 0;
            textPos = pos + new Vector(Spacing, Spacing);
        }

        public void SetMessage(String msg)
        {
            endAnimation = false;
            message = msg;
            messageArray = message.ToCharArray();
            messageToDraw = ""; // 表示メッセージを初期化
            charIndex = 0;
        }

        protected override void Move()
        {
            if (!endAnimation && frame % drawCharPeriod == 0) AddChar();
            base.Move();
        }

        public bool GetAnimIsEnd() => endAnimation;

        void AddChar()
        {
            if (messageArray == null) return;
            if (charIndex >= messageArray.Length)
            {
                endAnimation = true;
                return;
            }
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
