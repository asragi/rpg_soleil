using Soleil.Item;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    class ShopPanel : SelectablePanel
    {
        public readonly Vector ValuePosDiff = new Vector(300, 0);
        private string desc;
        public override string Desctiption => desc;

        // 価格表示
        string valueText;
        FontImage valueImage;

        public ShopPanel(ItemID id, int value, ShopItemList parent)
            :base(ItemDataBase.Get(id).Name, parent)
        {
            desc = ItemDataBase.Get(id).Description;

            // itemNum
            valueText = value.ToString();
            valueImage = new FontImage(ItemFont, LocalPos + parent.Pos + ValuePosDiff, DepthID.Message, true, 0);
            valueImage.Color = ColorPalette.DarkBlue;

            valueImage.Text = valueText;
        }
    }
}
