using System;

namespace Soleil.Event
{
    class FadeOutEvent
        :EventBase
    {
        Transition transition;
        bool started;
        public FadeOutEvent()
            :base()
        {
            transition = Transition.GetInstance();
        }

        public override void Execute()
        {
            if (!started)
            {
                started = true;
                transition.SetMode(TransitionMode.FadeOut);
                return;
            }
            if (transition.GetTransitionMode() == TransitionMode.None)
            {
                Console.WriteLine("end");
                Next();
            }
        }


    }
}
