namespace Soleil
{
    class ConversationWindow: Window
    {
        const TextureID Texture = TextureID.MessageWindow;
        const int x = 140;
        const int y = 300;

        protected override float Alpha => backImg.Alpha;
        protected override Vector SpaceVector => Vector.One;

        UIImage backImg;

        public ConversationWindow(WindowTag tag, WindowManager wm)
            : base(new Vector(x, y), tag, wm)
        {
            backImg = new UIImage(Texture, Pos, DiffPos, Depth);
            AddComponents(new[] { backImg });
        }
    }
}