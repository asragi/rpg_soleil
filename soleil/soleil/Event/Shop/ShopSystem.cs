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
    /// Shopイベントで用いるウィンドウ等の全体を管理するクラス．
    /// </summary>
    class ShopSystem : MenuComponent
    {
        readonly Vector DescriptionPos = new Vector(125, 35);
        readonly Vector MoneyPos = new Vector(90, 440);
        readonly Vector DetailWindowPos = new Vector(60, 100);
        private bool quitStart;
        int quitCount;
        public bool IsQuit { get; private set; }
        DescriptionWindow descriptionWindow;
        ShopItemList shopItemList;
        MoneyComponent moneyComponent;
        DetailWindow detailWindow;

        public ShopSystem(Dictionary<ItemID, int> values)
        {
            descriptionWindow = new DescriptionWindow();
            descriptionWindow.Text = "これはテストメッセージ";
            shopItemList = new ShopItemList(this, descriptionWindow, values);
            moneyComponent = new MoneyComponent(MoneyPos);
            detailWindow = new DetailWindow(DetailWindowPos);
        }

        public void Call()
        {
            IsQuit = false;
            shopItemList.Call();
            descriptionWindow.Call();
            moneyComponent.Call();
            detailWindow.Call();
            quitCount = 0;
            quitStart = false;
        }

        void Quit()
        {
            IsQuit = true;
        }

        void QuitStart()
        {
            quitStart = true;
            shopItemList.Quit();
            descriptionWindow.Quit();
            moneyComponent.Quit();
            detailWindow.Quit();
        }

        public void Input(Direction dir) => shopItemList.Input(dir);

        public override void Update()
        {
            base.Update();
            shopItemList.Update();
            descriptionWindow.Update();
            moneyComponent.Update();
            detailWindow.Update(shopItemList.SelectedPanel);
            QuitCheck();
            if (KeyInput.GetKeyPush(Key.B) && !quitStart) QuitStart();

            void QuitCheck()
            {
                if (quitStart)
                {
                    quitCount++;
                    if (quitCount > MenuSystem.FadeSpeed + 2) Quit();
                    return;
                }
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            shopItemList.Draw(d);
            descriptionWindow.Draw(d);
            moneyComponent.Draw(d);
            detailWindow.Draw(d);
        }
    }
}
