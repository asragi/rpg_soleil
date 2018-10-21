using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{
    /// <summary>
    /// Shopイベントで用いるウィンドウ等の全体を管理するクラス．
    /// </summary>
    class ShopSystem : MenuComponent
    {
        readonly Vector DescriptionPos = new Vector(125, 35);
        public bool IsQuit { get; private set; }
        MenuDescription menuDescription;
        ShopItemList shopItemList;

        public ShopSystem()
        {
            menuDescription = new MenuDescription(DescriptionPos);
            shopItemList = new ShopItemList(this, menuDescription);
        }

        public void Call()
        {
            IsQuit = false;
        }

        void Quit()
        {
            IsQuit = true;
        }
    }
}
