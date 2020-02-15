using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class CharacterDisappearEvent: EventBase
    {
        readonly MapCharacter character;

        public CharacterDisappearEvent(MapCharacter target)
        {
            character = target;
        }

        public override void Execute()
        {
            base.Execute();
            character.Visible = false;
            character.Exist = false;
            Next();
        }
    }
}
