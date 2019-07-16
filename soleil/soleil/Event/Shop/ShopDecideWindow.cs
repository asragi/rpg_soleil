using Soleil.Item;
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
        VariableWindow backWindow;
        TextImage itemName;
        TextImage price;
        Image currency;
        int purchaseNum;
        PriceSum priceSum;
        PurchaseNumDisplay numDisplay;

        public ShopDecideWindow(ItemID id, int _price) :
            base()
        {
            backWindow = new VariableWindow(WindowPos, WindowSize, WindowTag.A, WindowManager.GetInstance(), true);
            itemName = new TextImage(Font, ItemNamePos, PosDiff, Depth);
            itemName.Text = ItemDataBase.Get(id).Name;
            itemName.Color = ColorPalette.DarkBlue;
            price = new RightAlignText(Font, PricePos, PosDiff, Depth);
            price.Text = _price.ToString();
            price.Color = ColorPalette.DarkBlue;
            currency = new Image(
                TextureID.Currency,
                PricePos + new Vector(- Font.GetSize(_price.ToString()).X - 20, 5),
                PosDiff, Depth);

            priceSum = new PriceSum(PriceSumPos, PosDiff, _price, Font);
            numDisplay = new PurchaseNumDisplay(PurchaseNumDisplayPos, PosDiff, Font, NumDisplayDepth);
            AddComponents(new IComponent[] {
                itemName, price, currency, priceSum, numDisplay
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
            Console.WriteLine("ボタンが押されている");
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
            purchaseNum = MathEx.Clamp(purchaseNum, 99, 1);
            RefreshPurchaseNum();
        }

        private void RefreshPurchaseNum()
        {
            priceSum.SetPurchaseNum(purchaseNum);
            numDisplay.SetPurchaseNum(purchaseNum);
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
