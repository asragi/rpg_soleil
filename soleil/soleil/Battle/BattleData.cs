using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    struct BattleData
    {
        public CharacterType[] Enemies;

        public BattleData(params CharacterType[] enemies)
        {
            Enemies = enemies;
        }

        public static BattleData None = new BattleData(new CharacterType[] { });
    }
}
