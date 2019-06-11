using Soleil.Menu;

namespace Soleil
{
    /// <summary>
    /// 会話などの文章の文字部分のクラス．
    /// </summary>
    class MessageBox: MenuComponent
    {
        const int drawCharPeriod = 4; // nフレームごとに文字更新
        const DepthID Depth = DepthID.Message;
        private string message;
        public string Message { get => message; set { message = value; SetMessage(message); FinishAnim(); } }
        char[] messageArray;
        string messageToDraw; // 表示されるStringを保持する変数
        int charIndex; // char配列アクセス用index
        bool endAnimation;
        FontImage fontImage;
        int frame;

        public MessageBox(FontID font, Vector _pos, Vector _diffPos, bool isStatic, int fadeSpeed)
        {
            endAnimation = false;
            messageToDraw = "";
            fontImage = new FontImage(font, _pos, _diffPos, Depth, isStatic);
            fontImage.FadeSpeed = fadeSpeed;
            fontImage.Color = ColorPalette.DarkBlue;
            charIndex = 0;
            AddComponents(new[] { fontImage });
        }

        public void SetMessage(string msg)
        {
            endAnimation = false;
            message = msg;
            messageArray = Message.ToCharArray();
            messageToDraw = "";
            fontImage.Text = "";
            charIndex = 0;
        }

        public override void Update()
        {
            base.Update();
            frame++;
            if (!endAnimation && frame % drawCharPeriod == 0) AddChar();
        }

        public bool GetAnimIsEnd() => endAnimation;

        /// <summary>
        /// 強制的に文字列表示アニメーションを終了させる.
        /// </summary>
        public void FinishAnim()
        {
            messageToDraw = Message;
            fontImage.Text = Message;
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

        public static Vector GetSize(FontID font, string text)
            => (Vector)Resources.GetFont(font).MeasureString(text);
    }
}