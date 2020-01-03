using Soleil.Battle;
using Soleil.Map;
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
        public readonly MapName EntranceName;
        public readonly Vector EntrancePos;
        Dictionary<int, DungeonFloorEvent> events;
        EnemyAppearance[] EnemyAppearances;

        public DungeonData(
            Dictionary<int, DungeonFloorEvent> dict,
            EnemyAppearance[] enemyAppearances,
            MapName entrance, Vector entrancePos)
        {
            events = dict;
            EnemyAppearances = enemyAppearances;
            EntranceName = entrance;
            EntrancePos = entrancePos;
        }

        public int FloorNum => events.Keys.Max();
        public bool HasEvent(int floor) => events.ContainsKey(floor);
        public BattleData GetRandomBattle(int floor)
        {
            var targetList = EnemyAppearances.Where(s => s.Contains(floor)).ToArray();
            return targetList[Global.Random(0, targetList.Length - 1)].Enemies;
        }
        public DungeonFloorEvent GetEventClone(int floor)
            => (DungeonFloorEvent)events[floor].Clone();
        public DungeonFloorEvent GetEvent(int floor)
            => HasEvent(floor) ? events[floor] : NothingEvent.Nothing;
    }
}
