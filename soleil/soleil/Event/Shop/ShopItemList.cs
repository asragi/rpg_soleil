using Soleil.Item;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    class ShopItemList : BasicMenu
    {
        Dictionary<ItemID, int> values;
        public ShopItemList(MenuComponent parent, MenuDescription description, Dictionary<ItemID, int> _values)
            : base(parent, description)
        {
            values = _values;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var tmpPanels = new List<ShopPanel>();
            foreach (var item in values)
            {
                tmpPanels.Add(new ShopPanel(item.Key, item.Value, this));
            }
            return tmpPanels.ToArray();
        }
    }
}
