using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class CharacterMoveEvent : EventBase
    {
        WalkCharacter target;
        Direction dir;
        int duration, frame;
        bool waitEnd;
        public CharacterMoveEvent(WalkCharacter _target, Direction _dir, int _duration, bool _waitEnd = true)
        {
            frame = 0;
            (target, dir, duration, waitEnd) = (_target, _dir, _duration, _waitEnd);
        }

        public override void Execute()
        {
            base.Execute();
            frame++;
            // target.Move(dir);
            if (frame >= duration) Next();
        }

    }
}
