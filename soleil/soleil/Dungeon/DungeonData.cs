using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョンの構成や出来事，敵出現パターンなどを記述する構造．
    /// </summary>
    struct DungeonData
    {
        public int FloorNum => events.Last().Key;
        Dictionary<int, DungeonFloorEvent> events;
        public DungeonData(Dictionary<int, DungeonFloorEvent> dict)
        {
            events = dict;
        }

        public bool HasEvent(int floor) => events.ContainsKey(floor);
        public DungeonFloorEvent GetEvent(int floor)
            => (DungeonFloorEvent)events[floor].Clone();
    }
}
