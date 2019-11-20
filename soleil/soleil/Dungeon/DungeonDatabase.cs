using Soleil.Battle;
using Soleil.Dungeon.SearchFloorEvent;
using Soleil.Item;
using Soleil.Map;
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
                        {1, new ItemFind(ItemID.SilverWand) },
                    },
                    MapName.MagistolCol1, new Vector(1232, 1222)
                )
            );
        }

        public static DungeonData Get(DungeonName name) => data[name];
    }
}
