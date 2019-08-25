using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon.SearchFloorEvent
{
    /// <summary>
    /// ダンジョン探索中にアイテムを見つけるイベント
    /// </summary>
    class ItemFind : DungeonFloorEvent
    {
        public override object Clone()
            => new ItemFind();
    }
}
