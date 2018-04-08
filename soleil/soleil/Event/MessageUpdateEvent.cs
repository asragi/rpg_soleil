namespace Soleil.Event
{
    class MessageUpdateEvent
        :UpdateEventBase
    {
        WindowManager wm;
        WindowTag tag;
        public MessageUpdateEvent(WindowTag _tag)
            : base()
        {
            tag = _tag;
        }

        public override void Execute()
        {
            if (KeyInput.GetKeyPush(Key.A)) Next();
        }

    }
}
