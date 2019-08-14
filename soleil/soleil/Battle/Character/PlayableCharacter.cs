using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class PlayableCharacter : Character
    {
        public PlayableCharacter(int index, AbilityScore aScore, Battle.EquipSet equitps) : base(index)
        {
            Status = new CharacterStatus(aScore, 10000, equitps);
            commandSelect = new DefaultPlayableCharacterCommandSelect(charaIndex);
        }
    }
}
