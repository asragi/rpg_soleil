using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel : ItemPanelBase
    {
        private string desc;
        public override string Desctiption => desc;

        public ItemPanel(ItemID id, ItemList itemData, BasicMenu parent, bool active = true)
            :base(id, ItemDataBase.Get(id).Name, parent, active)
        {
            // Desctiption
            desc = ItemDataBase.Get(id).Description;
            // itemNum
            Val = itemData.GetItemNum(id);
        }
    }
}
