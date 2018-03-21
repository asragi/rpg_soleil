using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class Character
    {
        public CharacterStatus Status;
        List<Turn> turns;
        BattleField bf;
        public Character(BattleField bField)
        {
            bf = bField;

            //てきとう
            var aScore = new AbilityScore(100, 100, 100, 100, 100, 100);
            Status = new CharacterStatus(aScore, 10000);
            turns = new List<Turn>();
        }
    }
}
