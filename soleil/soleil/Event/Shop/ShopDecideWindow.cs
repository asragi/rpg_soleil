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
        static readonly Vector WindowPos = new Vector(80, 260);
        static readonly Vector WindowSize = new Vector(329, 130);
        static readonly Vector ItemNamePos = WindowPos + new Vector(35, 37);
        static readonly Vector PricePos = WindowPos + new Vector(277, 70);
        static readonly Vector PriceSumPos = WindowPos + new Vector(0, 160);
        static readonly FontID Font = FontID.CorpM;
        static readonly DepthID Depth = DepthID.Debug;
        public bool IsFocused { get; private set; }
        VariableWindow backWindow;
        TextImage itemName;
        TextImage price;
        Image currency;
        int purchaseNum;
        PriceSum priceSum;

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
            AddComponents(new IComponent[] {
                itemName, price, currency, priceSum
            });
        }

        public override void Call()
        {
            base.Call();
            IsFocused = true;
            backWindow.Call();
            purchaseNum = 1;
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
            priceSum.SetPurchaseNum(purchaseNum);
        }

        /// <summary>
        /// 購入するアイテムの合計価格表示
        /// </summary>
        class PriceSum: MenuComponent
        {
            private static readonly DepthID depth = DepthID.Message;
            TextImage priceSum;
            Image currency;
            int price;

            public PriceSum(Vector pos, Vector posDiff, int _price, FontID font)
            {
                price = _price;
                priceSum = new RightAlignText(font, pos, posDiff, depth);
                currency = new Image(TextureID.Currency, pos, posDiff, depth);

                AddComponents(new IComponent[] { priceSum, currency });
            }

            public void SetPurchaseNum(int num)
            {
                priceSum.Text = (num * price).ToString();
            }
        }

        /// <summary>
        /// 購入するアイテムの個数表示
        /// </summary>
        class PurchaseNum: MenuComponent
        {
            TextImage purchaseNum;
            TextImage purchaseNumText;
        }
    }
}
