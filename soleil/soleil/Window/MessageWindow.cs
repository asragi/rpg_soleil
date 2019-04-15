using System;

namespace Soleil
{
    class MessageWindow: VariableWindow, IMessageBox
    {
        MessageBox messageBox;

        public MessageWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm)
            : base(_pos, _size, tag, wm)
        {
            var targetPos = _pos + new Vector(Spacing);
            messageBox = new MessageBox(FontID.WhiteOutlineGrad, targetPos, DiffPos, false, FadeSpeed);
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
