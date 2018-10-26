using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    using EFunc = Func<double, double, double, double, double>;
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class ItemPanel : ItemPanelBase
    {
        public readonly Vector ItemNumPosDiff = new Vector(300, 0);

        private string desc;
        public override string Desctiption => desc;

        public ItemPanel(ItemID id, ItemList itemData, ItemMenu parent)
            :base(id, ItemDataBase.Get(id).Name, parent)
        {
            // Desctiption
            desc = ItemDataBase.Get(id).Description;
            // itemNum
            Val = itemData.GetItemNum(id);
        }
    }
}
