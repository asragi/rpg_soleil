﻿using Soleil.Dungeon.SearchFloorEvent;
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
        private static readonly NothingEvent nothing = new NothingEvent();
        private float SearchFindRate = 0.2f;
        private float EncounterRate = 0.6f;

        private readonly DungeonName name;
        public DungeonExecutor(DungeonName _name)
        {
            name = _name;
        }

        /// <summary>
        /// フロア探索時の処理結果に応じてModeを変更．
        /// </summary>
        public DungeonMode OnSearch(int floorNum)
        {
            var data = DungeonDatabase.Get(name);
            float rand = (float)Global.RandomDouble(0, 1.0);
            if (data.HasEvent(floorNum))
            {
                if (rand <= SearchFindRate)
                {
                    // 探索成功
                    return DecideReturnMode(data.GetEvent(floorNum));
                }
            }
            if (rand <= EncounterRate)
            {
                // 戦闘突入
                return DungeonMode.InitBattle;
            }
            // 何も起きない
            return DungeonMode.FirstWindow;
        }

        private DungeonMode DecideReturnMode(DungeonFloorEvent floorEvent)
        {
            switch (floorEvent)
            {
                case ItemFind _:
                    return DungeonMode.FindItem;
                case BattleEvent _:
                    return DungeonMode.InitBattle;
            }
            throw new ArgumentOutOfRangeException($"{floorEvent}のケースが実装されていません．");
        }
    }
}
