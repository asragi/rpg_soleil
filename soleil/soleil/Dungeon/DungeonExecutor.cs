using Soleil.Battle;
using Soleil.Dungeon.SearchFloorEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonのデータを読み込み実際に処理を行うクラス．
    /// </summary>
    class DungeonExecutor
    {
        private float SearchFindRate = 0.2f;
        private float EncounterRate = 0.7f;

        private readonly DungeonName name;
        public DungeonExecutor(DungeonName _name)
        {
            name = _name;
        }

        /// <summary>
        /// フロア探索時の処理結果に応じてModeを変更．
        /// </summary>
        public (DungeonMode, BattleData) OnSearch(int floorNum)
        {
            var data = DungeonDatabase.Get(name);
            float rand = (float)Global.RandomDouble(0, 1.0);
            var targetEvent = data.GetEvent(floorNum);
            if (data.HasEvent(floorNum) && !targetEvent.Achieved)
            {
                if (rand <= SearchFindRate)
                {
                    // 探索成功
                    targetEvent.Achieve();
                    return DecideReturnMode(targetEvent);
                }
            }
            if (rand <= EncounterRate)
            {
                // 戦闘突入
                return (DungeonMode.InitBattle, data.GetRandomBattle(floorNum));
            }
            // 何も起きない
            return (DungeonMode.FirstWindow, BattleData.None);
        }

        private (DungeonMode, BattleData) DecideReturnMode(DungeonFloorEvent floorEvent)
        {
            switch (floorEvent)
            {
                case ItemFind _:
                    return (DungeonMode.FindItem, BattleData.None);
                case BattleEvent b:
                    return (DungeonMode.InitBattle, b.BattleData);
            }
            throw new ArgumentOutOfRangeException($"{floorEvent}のケースが実装されていません．");
        }
    }
}
