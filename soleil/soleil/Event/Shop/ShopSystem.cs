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
            menuDescription.Text = "これはテストメッセージ";
            shopItemList = new ShopItemList(this, menuDescription);
        }

        public void Call()
        {
            IsQuit = false;
            shopItemList.Call();
            menuDescription.Call();
        }

        void Quit()
        {
            IsQuit = true;
            shopItemList.Quit();
            menuDescription.Quit();
        }

        public override void Update()
        {
            base.Update();
            shopItemList.Update();
            menuDescription.Update();
            if (KeyInput.GetKeyPush(Key.B)) Quit();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            shopItemList.Draw(d);
            menuDescription.Draw(d);
        }
    }
}
