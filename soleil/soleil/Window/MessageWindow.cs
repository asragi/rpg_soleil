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
        FontImage fontImage;

        public MessageWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm)
            : base(_pos,_size,tag,wm)
        {
            endAnimation = false;
            messageToDraw = "";
            fontImage = new FontImage(FontID.WhiteOutlineGrad, _pos + new Vector(Spacing, Spacing), DiffPos, DepthID.Message, false);
            fontImage.FadeSpeed = FadeSpeed;
            charIndex = 0;
            textPos = pos + new Vector(Spacing, Spacing);
        }

        public void SetMessage(String msg)
        {
            endAnimation = false;
            message = msg;
            messageArray = message.ToCharArray();
            messageToDraw = ""; // 表示メッセージを初期化
            fontImage.Text = "";
            charIndex = 0;
        }

        protected override void Move()
        {
            fontImage.Update();
            if (!endAnimation && frame % drawCharPeriod == 0) AddChar();
            base.Move();
        }

        public override void Call()
        {
            base.Call();
            fontImage.Call();
        }

        public override void Quit()
        {
            base.Quit();
            fontImage.Quit();
        }

        public bool GetAnimIsEnd() => endAnimation;

        /// <summary>
        /// 強制的に文字列表示アニメーションを終了させる.
        /// </summary>
        public void FinishAnim()
        {
            messageToDraw = message;
            fontImage.Text = message;
            charIndex = messageArray.Length;
            endAnimation = true;
        }

        void AddChar()
        {
            if (messageArray == null) return;
            if (charIndex >= messageArray.Length)
            {
                endAnimation = true;
                return;
            }
            messageToDraw += messageArray[charIndex];
            fontImage.Text = messageToDraw;
            charIndex++;
        }

        public override void DrawContent(Drawing d)
        {
            base.DrawContent(d);
            fontImage.Draw(d);
        }

        public static Vector GetProperSize(FontID font, string text)
        {
            return (Vector)Resources.GetFont(font).MeasureString(text) + new Vector(2 * Spacing, 2* Spacing);
        }
    }
}
