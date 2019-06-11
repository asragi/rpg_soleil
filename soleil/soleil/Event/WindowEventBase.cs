﻿namespace Soleil.Event
{
    abstract class WindowEventBase
        :EventBase
    {
        protected WindowManager Wm;
        protected Vector Pos, Size;
        public WindowTag Tag { get; private set; }
        public WindowEventBase(Vector _pos, Vector _size, WindowTag tag)
            :base()
        {
            Pos = _pos;
            Size = _size;
            Tag = tag;
            Wm = WindowManager.GetInstance();
        }
    }
}
