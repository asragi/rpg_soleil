using Soleil.Menu;

namespace Soleil
{
    class ConversationWindow: Window
    {
        const TextureID Texture = TextureID.MessageWindow;
        const int x = 140;
        const int y = 300;
        const FontID Font = FontID.KkBlack;

        protected override float Alpha => backImg.Alpha;
        protected override Vector SpaceVector => Vector.One;

        UIImage backImg;
        MessageBox messageBox;

        public ConversationWindow(WindowTag tag, WindowManager wm)
            : base(new Vector(x, y), tag, wm)
        {
            backImg = new UIImage(Texture, Pos, DiffPos, Depth);
            messageBox = new MessageBox(Font, Pos, DiffPos, true, FadeSpeed);
            AddComponents(new IComponent[] { backImg, messageBox });
        }

        public bool GetAnimIsEnd() => messageBox.GetAnimIsEnd();
        public void FinishAnim() => messageBox.FinishAnim();
        public void SetMessage(string text) => messageBox.SetMessage(text);
    }
}