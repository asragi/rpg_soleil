using Soleil.Item;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    class ShopPanel : ItemPanelBase
    {
        public readonly Vector ValuePosDiff = new Vector(300, 0);
        private string desc;
        public override string Desctiption => desc;
        public int Price { get; private set; }

        public ShopPanel(ItemID id, int value, ShopItemList parent)
            :base(id, ItemDataBase.Get(id).Name, parent)
        {
            desc = ItemDataBase.Get(id).Description;
            Price = value;

            Val = Price;
            ValFont = FontID.KkMini;
        }
    }
}
