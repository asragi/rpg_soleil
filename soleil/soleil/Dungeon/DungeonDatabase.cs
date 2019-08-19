using Soleil.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    enum DungeonName
    {
        MagistolUnderground,
        size,
    }

    /// <summary>
    /// ミニダンジョンの構成を記述・管理するクラス．
    /// </summary>
    static class DungeonDatabase
    {
        private static readonly Dictionary<DungeonName, DungeonData> data;

        static DungeonDatabase()
        {
            data = new Dictionary<DungeonName, DungeonData>();

            data.Add(DungeonName.MagistolUnderground, new DungeonData(
                new Dictionary<int, DungeonFloorEvent>()
                {
                    {1, new BattleEvent(new[] { EnemyName.Test, EnemyName.Test }) },
                    {3, new BattleEvent(new[] { EnemyName.Test }) }
                }
                )
            );
        }

        public static DungeonData Get(DungeonName name) => data[name];
    }
}
