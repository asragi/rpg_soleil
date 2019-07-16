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
    /// <summary>
    /// 購入数を選択して購入を決定するウィンドウ
    /// </summary>
    class ShopDecideWindow: MenuComponent
    {
        static readonly Vector PosDiff = Window.DiffPos;
        static readonly Vector WindowPos = new Vector(80, 280);
        static readonly Vector WindowSize = new Vector(329, 130);
        static readonly Vector ItemNamePos = WindowPos + new Vector(35, 37);
        static readonly Vector PricePos = WindowPos + new Vector(277, 70);
        static readonly Vector PurchaseNumDisplayPos = WindowPos + new Vector(0, 120);
        static readonly Vector PriceSumPos = PurchaseNumDisplayPos + new Vector(160, 0);
        static readonly DepthID NumDisplayDepth = DepthID.Message;
        static readonly FontID Font = FontID.CorpM;
        static readonly DepthID Depth = DepthID.Debug;
        public bool IsFocused { get; private set; }
        // 参照
        ShopItemList shopList;
        // -- 購入上限を監視するための参照たち
        ShopStorage storage;
        ItemList itembag;
        MoneyWallet moneyWallet;
        int selectedIndex;
        // UIコンポーネント
        VariableWindow backWindow;
        TextImage itemName;
        TextImage priceImg;
        Image currency;
        PriceSum priceSum;
        PurchaseNumDisplay numDisplay;
        // 購入状態管理変数
        int purchaseNum;
        int price;
        ItemID target;

        public ShopDecideWindow(ItemID id, int _price, int index,
            ShopItemList itemList, ShopStorage str, ItemList bag, MoneyWallet wallet) :
            base()
        {
            // 代入及び参照
            price = _price;
            target = id;
            shopList = itemList;
            storage = str;
            itembag = bag;
            moneyWallet = wallet;
            selectedIndex = index;

            // 表示コンポーネントのインスタンス生成
            backWindow = new VariableWindow(WindowPos, WindowSize, WindowTag.A, WindowManager.GetInstance(), true);
            itemName = new TextImage(Font, ItemNamePos, PosDiff, Depth);
            itemName.Text = ItemDataBase.Get(id).Name;
            itemName.Color = ColorPalette.DarkBlue;
            priceImg = new RightAlignText(Font, PricePos, PosDiff, Depth);
            priceImg.Text = _price.ToString();
            priceImg.Color = ColorPalette.DarkBlue;
            currency = new Image(
                TextureID.Currency,
                PricePos + new Vector(- Font.GetSize(_price.ToString()).X - 20, 5),
                PosDiff, Depth);

            priceSum = new PriceSum(PriceSumPos, PosDiff, _price, Font);
            numDisplay = new PurchaseNumDisplay(PurchaseNumDisplayPos, PosDiff, Font, NumDisplayDepth);
            AddComponents(new IComponent[] {
                itemName, priceImg, currency, priceSum, numDisplay
            });
        }

        public override void Call()
        {
            base.Call();
            IsFocused = true;
            backWindow.Call();
            purchaseNum = 1;
            RefreshPurchaseNum();
        }

        public override void Quit()
        {
            base.Quit();
            IsFocused = false;
            backWindow.Quit();
        }

        public void OnInputSubmit()
        {
            shopList.Purchase(purchaseNum, price, target);
            Quit();
        }

        public void OnInputCancel()
        {
            Quit();
        }
        
        public void Input(Direction d)
        {
            if (d == Direction.R)
            {
                purchaseNum++;
            }
            else if (d == Direction.L)
            {
                purchaseNum--;
            }
            purchaseNum = MathEx.Clamp(purchaseNum, GetPurchaseMax(), 1);
            RefreshPurchaseNum();

            if (KeyInput.GetKeyPush(Key.A)) OnInputSubmit();
        }

        private void RefreshPurchaseNum()
        {
            priceSum.SetPurchaseNum(purchaseNum);
            numDisplay.SetPurchaseNum(purchaseNum);
        }

        private int GetPurchaseMax()
        {
            // 所持金による上限
            int moneyLimit = moneyWallet.Val / price;
            // アイテム所持数による上限
            int bagLimit = 99 - itembag.GetItemNum(target);
            // お店の在庫による上限
            int storageLimit = storage.GetStockNum(selectedIndex);
            return Math.Min(moneyLimit, Math.Min(bagLimit, storageLimit));
        }

        /// <summary>
        /// 購入するアイテムの合計価格表示
        /// </summary>
        class PriceSum: MenuComponent
        {
            private static readonly DepthID depth = DepthID.Message;
            private static readonly Vector CurrencyHeight = new Vector(0, 5);
            private static readonly Vector PriceDistance = new Vector(150, 0);
            TextImage priceSum;
            Image currency;
            int price;

            public PriceSum(Vector pos, Vector posDiff, int _price, FontID font)
            {
                price = _price;
                priceSum = new RightAlignText(font, pos + PriceDistance, posDiff, depth);
                currency = new Image(TextureID.Currency, pos + CurrencyHeight, posDiff, depth);
                AddComponents(new IComponent[] { priceSum, currency });
            }

            public void SetPurchaseNum(int num)
            {
                string text = (num * price).ToString();
                priceSum.Text = text;
            }
        }

        /// <summary>
        /// 購入するアイテムの個数表示
        /// </summary>
        class PurchaseNumDisplay: MenuComponent
        {
            private static readonly Vector NumDistance = new Vector(130, 0);
            private const string PurchaseText = "購入数";
            TextImage purchaseNum;
            TextImage purchaseNumText;

            public PurchaseNumDisplay(Vector pos, Vector posDiff, FontID font, DepthID _depth)
            {
                purchaseNum = new RightAlignText(font, pos + NumDistance, posDiff, _depth);
                purchaseNumText = new TextImage(font, pos, posDiff, _depth);
                purchaseNumText.Color = ColorPalette.AliceBlue;
                purchaseNumText.Text = PurchaseText;
                AddComponents(new[] { purchaseNum, purchaseNumText });
            }

            public void SetPurchaseNum(int num)
            {
                purchaseNum.Text = num.ToString();
            }
        }
    }
}
