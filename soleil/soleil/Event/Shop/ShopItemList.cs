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
        public ShopItemList(MenuComponent parent, MenuDescription description, Dictionary<ItemID, int> values)
            : base(parent, description)
        {
            Panels = SetPanel();

            ShopPanel[] SetPanel(){
                var tmpPanels = new List<ShopPanel>();
                foreach (var item in values)
                {
                    tmpPanels.Add(new ShopPanel(item.Key, item.Value, this));
                }
                return tmpPanels.ToArray();
            }
        }

        public override void Call()
        {
            base.Call();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
