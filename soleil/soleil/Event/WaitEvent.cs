using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class WaitEvent: EventBase
    {
        private readonly int waitFrame;
        int frame;

        public WaitEvent(int _wait)
        {
            waitFrame = _wait;
            frame = 0;
        }

        public override void Execute()
        {
            base.Execute();
            frame++;
            if (frame > waitFrame) Next();
        }
    }
}
