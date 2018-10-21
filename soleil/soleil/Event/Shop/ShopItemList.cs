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
        public ShopItemList(MenuComponent parent, MenuDescription description)
            : base(parent, description)
        {
            Panels = new ShopPanel[]
            {
                new ShopPanel(Item.ItemID.d0, 2000, this),
                new ShopPanel(Item.ItemID.Stone, 200, this),
            };
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
