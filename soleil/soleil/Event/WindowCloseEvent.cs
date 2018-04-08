namespace Soleil.Event
{
    class WindowCloseEvent
        :EventBase
    {
        int tag;
        WindowManager wm;
        public WindowCloseEvent(int _tag, EventManager em)
            :base(em)
        {
            tag = _tag;
            wm = WindowManager.GetInstance();
        }

        public override void Execute()
        {
            wm.Destroy(tag);
            Next();
        }
    }
}
