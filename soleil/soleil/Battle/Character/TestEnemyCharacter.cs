using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class TestEnemyCharacter : Character
    {
        public TestEnemyCharacter(int index) : base(index)
        {
            //てきとう
            var aScore = new AbilityScore(50, 5, 5, 5, 5, 5);
            Status = new CharacterStatus(aScore, 10000);
            commandSelect = new DefaultCharacterCommandSelect(charaIndex);
        }
    }
}
