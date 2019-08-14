using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class PlayableCharacter : Character
    {
        public PlayableCharacter(int index, AbilityScore aScore) : base(index)
        {
            Status = new CharacterStatus(aScore, 10000);
            commandSelect = new DefaultPlayableCharacterCommandSelect(charaIndex);
        }
    }
}
