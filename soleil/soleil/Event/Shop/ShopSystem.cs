using Soleil.Item;
using Soleil.Menu;
using Soleil.Menu.Detail;
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
        private readonly static Vector PossessPos = new Vector(80, 440);
        readonly Vector MoneyPos = PossessPos + new Vector(172, 0);
        readonly Vector DetailWindowPos = new Vector(100, 163);
        private bool quitStart;
        int quitCount;
        public bool IsQuit { get; private set; }
        DescriptionWindow descriptionWindow;
        ShopItemList shopItemList;
        MoneyComponent moneyComponent;
        PossessNum possessNum;
        DetailWindow detailWindow;
        public bool Purchased;

        public ShopSystem(ShopName name)
        {
            descriptionWindow = new DescriptionWindow();
            descriptionWindow.Text = "これはテストメッセージ";
            shopItemList = new ShopItemList(this, descriptionWindow, name);
            moneyComponent = new MoneyComponent(MoneyPos, Vector.Zero);
            possessNum = new PossessNum(PossessPos);
            detailWindow = new DetailWindow(DetailWindowPos);
        }

        public override void Call()
        {
            base.Call();
            IsQuit = false;
            shopItemList.Call();
            descriptionWindow.Call();
            moneyComponent.Call();
            detailWindow.Call();
            possessNum.Call();
            quitCount = 0;
            Purchased = false;
            quitStart = false;
        }

        public override void Quit()
        {
            base.Quit();
            IsQuit = true;
        }

        void QuitStart()
        {
            quitStart = true;
            Purchased = shopItemList.Purchased;
            shopItemList.Quit();
            descriptionWindow.Quit();
            moneyComponent.Quit();
            detailWindow.Quit();
            possessNum.Quit();
        }

        public void Input(Direction dir)
        {
            if (quitStart) return;
            shopItemList.Input(dir);
        }

        public override void Update()
        {
            base.Update();
            shopItemList.Update();
            descriptionWindow.Update();
            moneyComponent.Update();
            possessNum.Refresh(shopItemList.SelectedPanel);
            possessNum.Update();
            detailWindow.Refresh(shopItemList.SelectedPanel);
            detailWindow.Update();
            QuitCheck();
            if (KeyInput.GetKeyPush(Key.B) && !quitStart) OnInputCancel();

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
            possessNum.Draw(d);
            descriptionWindow.Draw(d);
            moneyComponent.Draw(d);
            detailWindow.Draw(d);
        }


        private void OnInputCancel()
        {
            if (quitStart) return;
            shopItemList.OnInputCancel();
            if (shopItemList.ReadyForEnd) QuitStart();
        }
    }
}
