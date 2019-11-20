using Soleil.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    struct EnemyAppearance
    {
        int from;
        int to;
        public BattleData Enemies;

        public EnemyAppearance(int _from, int _to, BattleData enemies)
        {
            from = _from;
            to = _to;
            Enemies = enemies;
        }

        public bool Contains(int floor)
        {
            return (floor >= from) && (floor <= to);
        }
    }
}
