using System;

namespace Soleil.Event
{
    abstract class WindowEventBase
        :EventBase
    {
        protected WindowManager Wm;
        protected Vector Size;
        protected Func<Vector> PosFunc;
        public WindowTag Tag { get; private set; }
        public WindowEventBase(Vector _pos, Vector _size, WindowTag tag)
            :base()
        {
            PosFunc = () => _pos;
            Size = _size;
            Tag = tag;
            Wm = WindowManager.GetInstance();
        }
    }
}
