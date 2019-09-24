﻿using Soleil.Dungeon.SearchFloorEvent;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonでアイテムを見つけた時の演出・処理．
    /// </summary>
    class ItemFindEvent
    {
        DungeonMaster master;
        DungeonState state;

        public ItemFindEvent(
            DungeonMaster _master,
            DungeonState _state)
        {
            master = _master;
            state = _state;
        }

        public void Exec()
        {
            // Add Item
            var dungeonData = DungeonDatabase.Get(state.Name);
            var eventData = (ItemFind)dungeonData.GetEvent(state.FloorNum);
            var itemBag = PlayerBaggage.GetInstance().Items;
            itemBag.AddItem(eventData.ID, eventData.Num);
            // Change Mode
            master.Mode = DungeonMode.FirstWindow;
        }
    }
}