﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// ダンジョン探索時の結果が未設定などで何も起こらないときのイベント．
    /// </summary>
    class NothingEvent : DungeonFloorEvent
    {
        public override object Clone() => new NothingEvent();
    }
}