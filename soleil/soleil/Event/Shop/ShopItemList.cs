using Soleil.Item;
using Soleil.Map;
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
        protected override Vector WindowPos => new Vector(440, 100);
        Dictionary<ItemID, int> values;
        MoneyWallet moneyWallet;
        ItemList itemList;
        public bool Purchased;

        public ShopItemList(MenuComponent parent, MenuDescription description, Dictionary<ItemID, int> _values)
            : base(parent, description)
        {
            var p = PlayerBaggage.GetInstance();
            moneyWallet = p.MoneyWallet;
            itemList = p.Items;
            values = _values;
            Init();
        }

        public override void Call()
        {
            base.Call();
            Purchased = false;
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

        public override void OnInputSubmit()
        {
            var decidedPanel = (ShopPanel)Panels[Index];
            var decidedPrice = decidedPanel.Price;
            if (moneyWallet.HasEnough(decidedPrice))
            {
                // 購入成功
                Console.WriteLine("購入成功");
                Purchased = true;
                moneyWallet.Consume(decidedPrice);
                itemList.AddItem(decidedPanel.ID);
            }
            else
            {
                // 所持金が足りない
                Console.WriteLine("所持金が足りない");
            }
        }

        public override void OnInputCancel() { }
    }
}
