using Soleil.Menu;

namespace Soleil
{
    class ConversationWindow: Window, IMessageBox
    {
        const TextureID Texture = TextureID.ConversationWindow;
        const int x = 140;
        const int y = 300;
        const FontID Font = FontID.CorpM;

        protected override float Alpha => backImg.Alpha;
        readonly Vector contentPos = new Vector(75, 60);
        protected override Vector SpaceVector => contentPos;
        readonly Vector NamePos = new Vector(x + 80, y + 8);

        UIImage backImg;
        MessageBox messageBox;
        FontImage nameImg;

        public ConversationWindow(WindowTag tag, WindowManager wm)
            : base(new Vector(x, y), tag, wm)
        {
            backImg = new UIImage(Texture, Pos, DiffPos, Depth);
            nameImg = new FontImage(Font, NamePos, Depth);
            nameImg.Color = ColorPalette.AliceBlue;
            messageBox = new MessageBox(Font, ContentPos, DiffPos, true, FadeSpeed);
            AddComponents(new IComponent[] { backImg, messageBox, nameImg });
            SetMessage("テストメッセージ！テストメッセージ！\nテストメッセージ！\nテストメッセージ！");
        }

        public bool GetAnimIsEnd() => messageBox.GetAnimIsEnd();
        public void FinishAnim() => messageBox.FinishAnim();
        public void SetMessage(string text) => messageBox.SetMessage(text);
        public void SetName(string name) => nameImg.Text = name;
    }
}