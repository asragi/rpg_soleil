using Soleil.Menu;

namespace Soleil
{
    class ConversationWindow: Window, IMessageBox
    {
        const TextureID Texture = TextureID.MessageWindow;
        const int x = 140;
        const int y = 300;
        const FontID Font = FontID.KkBlack;

        protected override float Alpha => backImg.Alpha;
        readonly Vector contentPos = new Vector(75, 60);
        protected override Vector SpaceVector => contentPos;

        UIImage backImg;
        MessageBox messageBox;

        public ConversationWindow(WindowTag tag, WindowManager wm)
            : base(new Vector(x, y), tag, wm)
        {
            backImg = new UIImage(Texture, Pos, DiffPos, Depth);
            messageBox = new MessageBox(Font, ContentPos, DiffPos, true, FadeSpeed);
            AddComponents(new IComponent[] { backImg, messageBox });
            SetMessage("テストメッセージ！テストメッセージ！\nテストメッセージ！\nテストメッセージ！");
        }

        public bool GetAnimIsEnd() => messageBox.GetAnimIsEnd();
        public void FinishAnim() => messageBox.FinishAnim();
        public void SetMessage(string text) => messageBox.SetMessage(text);
    }
}