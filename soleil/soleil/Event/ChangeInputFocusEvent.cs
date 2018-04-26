using Soleil.Map;
using System;

namespace Soleil.Event
{
    class ChangeInputFocusEvent
        :EventBase
    {
        MapInputManager mim;
        InputFocus focus;
        public ChangeInputFocusEvent(InputFocus _focus)
            : base()
        {
            focus = _focus;
            mim = MapInputManager.GetInstance();
        }

        public override void Execute()
        {
            mim.SetFocus(focus);
            Next();
        }
    }
}
