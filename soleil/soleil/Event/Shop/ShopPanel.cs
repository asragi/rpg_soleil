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
        public readonly Vector CurrencyPosDiff = new Vector(5, 12);
        private string desc;
        public override string Desctiption => desc;
        public override Vector ItemNumPosDiff => base.ItemNumPosDiff - new Vector(30,6); // ちゃんと下揃えを計算するべきだが，面倒くさかった．
        public int Price { get; private set; }

        FontImage currency;

        public ShopPanel(ItemID id, int value, ShopItemList parent)
            :base(id, ItemDataBase.Get(id).Name, parent)
        {
            desc = ItemDataBase.Get(id).Description;
            Price = value;

            Val = Price;
            ValFont = FontID.CorpM;
            currency = new FontImage(FontID.CorpM, LocalPos + parent.Pos, DepthID.Message, true, 0);
            currency.Text = Map.MoneyWallet.Currency;
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
