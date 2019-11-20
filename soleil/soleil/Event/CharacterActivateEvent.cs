using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class CharacterActivateEvent: EventBase
    {
        private readonly PersonParty party;
        private readonly CharaName name;
        private readonly bool active;

        public CharacterActivateEvent(PersonParty p, CharaName _name, bool _active)
        {
            party = p;
            name = _name;
            active = _active;
        }

        public override void Execute()
        {
            base.Execute();
            party.SetActive(name, active);
            Next();
        }
    }
}
