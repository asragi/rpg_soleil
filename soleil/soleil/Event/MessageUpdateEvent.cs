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
            wm = WindowManager.GetInstance();
        }

        public override void Execute()
        {
            if (KeyInput.GetKeyPush(Key.A))
            {
                ReactToInput();
            }
        }

        private void ReactToInput()
        {
            if (wm.GetIsMessageWindowAnimFinished(tag))
            {
                Next();
                return;
            }
            wm.FinishMessageWindowAnim(tag);
        }
    }
}
