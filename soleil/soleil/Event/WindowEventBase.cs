namespace Soleil.Event
{
    abstract class WindowEventBase
        :EventBase
    {
        protected WindowManager Wm;
        protected Vector Pos, Size;
        public int Tag { get; private set; }
        public WindowEventBase(Vector _pos, Vector _size, int tag, EventManager e)
            :base(e)
        {
            Pos = _pos;
            Size = _size;
            Wm = WindowManager.GetInstance();
        }
    }
}
