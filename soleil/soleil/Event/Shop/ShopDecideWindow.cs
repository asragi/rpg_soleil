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
        static readonly Vector WindowPos = new Vector(312, 202);
        static readonly Vector WindowSize = new Vector(329, 130);
        static readonly Vector ItemNamePos = WindowPos + new Vector(35, 37);
        static readonly Vector PricePos = WindowPos + new Vector(277, 70);
        static readonly FontID Font = FontID.CorpM;
        static readonly DepthID Depth = DepthID.Debug;
        public bool IsFocused { get; private set; }
        VariableWindow backWindow;
        TextImage itemName;
        TextImage price;
        Image currency;

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
            AddComponents(new IComponent[] {
                itemName, price, currency,
            });
        }

        public override void Call()
        {
            base.Call();
            IsFocused = true;
            backWindow.Call();
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


        /// <summary>
        /// 購入するアイテムの合計価格表示
        /// </summary>
        class PriceSum: MenuComponent
        {
            TextImage priceSum;
            Image currency;
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
