using Microsoft.Xna.Framework.Graphics;
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
        public readonly Vector CurrencyPosDiff;
        private string desc;
        public override string Desctiption => desc;
        public override Vector ItemNumPosDiff => base.ItemNumPosDiff; // ちゃんと下揃えを計算するべきだが，面倒くさかった．
        public int Price { get; private set; }

        Image currency;

        public ShopPanel(ItemID id, int value, bool active, ShopItemList parent)
            : base(id, ItemDataBase.Get(id).Name, parent, active)
        {
            var font = FontID.CorpM;
            desc = ItemDataBase.Get(id).Description;
            Price = value;

            Val = Price;
            ValFont = font;
            currency = new Image(TextureID.Currency, LocalPos + parent.Pos, DepthID.Message, isStatic: true);

            CurrencyPosDiff = new Vector(-Resources.GetFont(font).MeasureString(Price.ToString()).X - 20, 5);
        }

        public override void Update()
        {
            base.Update();
            currency.Alpha = BasicMenu.Alpha;
            currency.Pos = BasicMenu.Pos + Spacing + LocalPos + ItemNumPosDiff + CurrencyPosDiff;
            currency.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            currency.Draw(d);
        }
    }
}
