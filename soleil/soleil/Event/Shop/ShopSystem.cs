using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    class ShopSystem : MenuComponent
    {
        readonly Vector DescriptionPos = new Vector(125, 35);
        MenuDescription menuDescription;
        ShopItemList shopItemList;

        public ShopSystem()
        {
            menuDescription = new MenuDescription(DescriptionPos);
            shopItemList = new ShopItemList(this, menuDescription);
        }
    }
}
