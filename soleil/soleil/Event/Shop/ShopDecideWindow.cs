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
        static readonly Vector PosDiff = Vector.Zero;
        static readonly Vector WindowPos = new Vector(312, 222);
        static readonly Vector WindowSize = new Vector(329, 103);
        static readonly Vector ItemNamePos = WindowPos + new Vector(35, 27);
        static readonly Vector PricePos = WindowPos + new Vector(277, 60);
        static readonly FontID Font = FontID.CorpM;
        static readonly DepthID Depth = DepthID.MenuTop;
        VariableWindow backWindow;
        TextImage itemName;
        TextImage price;
        Image currencyTop;

        public ShopDecideWindow() :
            base()
        {
            backWindow = new VariableWindow(WindowPos, WindowSize, WindowTag.A, WindowManager.GetInstance(), true);
            itemName = new TextImage(Font, ItemNamePos, PosDiff, Depth);
            price = new RightAlignText(Font, PricePos, PosDiff, Depth);
            AddComponents(new IComponent[] {
                backWindow, itemName,
                price, currencyTop, 
            });
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
