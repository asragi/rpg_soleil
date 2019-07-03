using System;

namespace Soleil
{
    class MessageWindow: VariableWindow, IMessageBox
    {
        MessageBox messageBox;
        public static FontID DefaultFont = FontID.CorpM;
        public string Text { get => messageBox.Message; set => messageBox.Message = value; }

        public MessageWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm, bool isStatic = false)
            : base(_pos, _size, tag, wm, isStatic)
        {
            var targetPos = Pos + new Vector(Spacing);
            messageBox = new MessageBox(DefaultFont, targetPos, DiffPos, isStatic, FadeSpeed);
            AddComponents(new[] { messageBox });
        }

        public void SetMessage(string msg) => messageBox.SetMessage(msg);
        protected override void Move() => messageBox.Update();
        public bool GetAnimIsEnd() => messageBox.GetAnimIsEnd();
        public void FinishAnim() => messageBox.FinishAnim();

        public static Vector GetProperSize(FontID font, string text)
        {
            return MessageBox.GetSize(font, text) + new Vector(2 * Spacing, 2* Spacing);
        }
    }
}
