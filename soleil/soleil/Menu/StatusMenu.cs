using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusMenu : MenuChild
    {
        MenuChild calledFrom;
        MenuCharacterPanel[] menuCharacterPanels;
        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;
        MenuSystem menuSystem;

        int index;
        public StatusMenu(MenuSystem parent)
            :base(parent)
        {
            menuSystem = parent;
            index = 0;
            menuCharacterPanels = new MenuCharacterPanel[2];
            menuCharacterPanels[0] = new MenuCharacterPanel(new Vector(290, 120), TextureID.MenuLune);
            menuCharacterPanels[1] = new MenuCharacterPanel(new Vector(540, 120), TextureID.MenuSun);
        }

        /// <summary>
        /// メニューが立ち上がる時の処理
        /// </summary>
        public override void Call()
        {
            foreach (var panel in menuCharacterPanels)
            {
                panel.FadeIn();
            }
        }

        /// <summary>
        /// メニューが閉じるときの処理
        /// </summary>
        public override void Quit()
        {
            foreach (var panel in menuCharacterPanels)
            {
                panel.FadeOut();
            }
        }

        public override void Update()
        {
            base.Update();

            // Panel Update
            foreach (var panel in menuCharacterPanels)
            {
                panel.Update();
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            // Panel Draw
            foreach (var panel in menuCharacterPanels)
            {
                panel.Draw(d);
            }
        }

        /// <summary>
        /// MenuSystemの他のウィンドウからFocusを移される場合．
        /// </summary>
        public void FocusTo(MenuChild from)
        {
            IsActive = true;
            from.IsActive = false;
            calledFrom = from;
        }

        // Input
        public override void OnInputRight() {
            index++;
            index = (menuCharacterPanels.Length + index) % menuCharacterPanels.Length;
        }

        public override void OnInputLeft() {
            index--;
            index = (menuCharacterPanels.Length + index) % menuCharacterPanels.Length;
        }

        public override void OnInputSubmit() {
            // ステータス選択以前に選ばれていた項目を見る．
            menuSystem.CallChild((MenuName)menuSystem.Index);
            IsActive = false;
        }

        public override void OnInputCancel() {
            if(calledFrom is MenuSystem)
            {
                ReturnParent();
            }
            else
            {
                calledFrom.Call();
                IsActive = false;
            }
        }
    }
}
