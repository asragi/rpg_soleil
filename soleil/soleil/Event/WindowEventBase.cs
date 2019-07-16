using System;

namespace Soleil.Event
{
    abstract class WindowEventBase
        :EventBase
    {
        private static readonly Vector CharacterMessagePos = new Vector(-50, -200);
        protected WindowManager Wm = WindowManager.GetInstance();
        protected Vector Size;
        protected Func<Vector> PosFunc;
        public WindowTag Tag { get; private set; }

        public WindowEventBase(ICollideObject obj, Vector size)
        {
            PosFunc = () => obj.GetPosition() + CharacterMessagePos;
            Size = size;
            Tag = WindowTag.A;
        }

        /// <summary>
        /// 座標を指定してウィンドウを出す．
        /// </summary>
        public WindowEventBase(Vector _pos, Vector _size, WindowTag tag)
        {
            PosFunc = () => _pos;
            Size = _size;
            Tag = tag;
        }
    }
}
