﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonの階層などの状態を管理するクラス．
    /// </summary>
    class DungeonState
    {
        public int FloorNum { get; private set; }
        public readonly DungeonName Name;

        public DungeonState(
            DungeonName name, int initFloor = 1)
        {
            Name = name;
            FloorNum = initFloor;
        }

        public void GoNext() => FloorNum++;
    }
}
