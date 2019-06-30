using Soleil.Map;
using System;

namespace Soleil.Event
{
    class ChangeInputFocusEvent
        :EventBase
    {
        MapInputManager mim;
        InputFocus focus;
        public ChangeInputFocusEvent(InputFocus _focus, MapInputManager _mim)
            : base()
        {
            focus = _focus;
            mim = _mim;
        }

        public override void Execute()
        {
            mim.SetFocus(focus);
            Next();
        }
    }
}
