using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class PlaySoundEvent : EventBase
    {
        SoundID id;

        public PlaySoundEvent(SoundID s)
        {
            id = s;
        }

        public override void Execute()
        {
            base.Execute();
            Audio.PlaySound(id);
            Next();
        }
    }
}
