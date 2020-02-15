using System;

namespace Soleil.Event
{
    class FadeInEvent
        : EventBase
    {
        Transition transition;
        bool started;
        public FadeInEvent()
            : base()
        {
            transition = Transition.GetInstance();
        }

        public override void Execute()
        {
            if (!started)
            {
                started = true;
                transition.SetMode(TransitionMode.FadeIn);
                return;
            }
            if (transition.GetTransitionMode() == TransitionMode.None)
            {
                Next();
            }
        }


    }
}
