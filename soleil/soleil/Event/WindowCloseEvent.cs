namespace Soleil.Event
{
    class WindowCloseEvent
        :EventBase
    {
        WindowTag tag;
        WindowManager wm;
        public WindowCloseEvent(WindowTag _tag, EventManager em)
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
