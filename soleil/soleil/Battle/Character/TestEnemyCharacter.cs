using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestEnemyCharacter : Character
    {
        public TestEnemyCharacter(int index) : base(index)
        {
            //てきとう
            var aScore = new AbilityScore(1000, 100, 100, 100, 100, 100);
            Status = new CharacterStatus(aScore, 10000);
            commandSelect = new DefaultCharacterCommandSelect(charaIndex);
        }
    }
}
