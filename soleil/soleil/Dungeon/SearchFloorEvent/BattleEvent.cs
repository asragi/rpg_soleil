﻿using Soleil.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonで戦闘が発生した時のイベントの内容を記述するクラス．
    /// </summary>
    class BattleEvent: DungeonFloorEvent
    {
        public readonly BattleData BattleData;
        public BattleEvent(BattleData battleData)
        {
            BattleData = battleData;
        }

        public override string DisplayName => "戦闘";
        public override object Clone() => new BattleEvent(BattleData);
    }
}
