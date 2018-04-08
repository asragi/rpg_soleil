namespace Soleil.Event
{
    class MessageUpdateEvent
        :UpdateEventBase
    {
        WindowManager wm;
        int tag;
        public MessageUpdateEvent(int _tag, EventManager e)
            : base(e)
        {
            tag = _tag;
        }

        public override void Execute()
        {
            if (KeyInput.GetKeyPush(Key.A)) Next();
        }

    }
}
