using Soleil.Item;
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
        public readonly ItemID ID;
        public readonly int Num;
        public ItemFind(ItemID id, int num = 1)
        {
            ID = id;
            Num = num;
        }

        public override object Clone()
            => new ItemFind(ID, Num);
    }
}
