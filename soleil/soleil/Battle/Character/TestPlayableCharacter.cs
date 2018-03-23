using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestPlayableCharacter : Character
    {
        public TestPlayableCharacter(BattleField bField, int index) : base(bField, index)
        {
            //てきとう
            var aScore = new AbilityScore(100, 100, 100, 100, 100, 100);
            Status = new CharacterStatus(aScore, 10000);
            commandSelect = new DefaultPlayableCharacterCommandSelect();
        }
    }
}
