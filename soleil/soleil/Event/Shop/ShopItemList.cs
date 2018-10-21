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
            Panels = new SelectablePanel[0];
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
