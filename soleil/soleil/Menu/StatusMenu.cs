using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusMenu : MenuChild
    {
        MenuCharacterPanel[] menuCharacterPanels;
        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;

        public StatusMenu(MenuComponent parent)
            :base(parent)
        {
            menuCharacterPanels = new MenuCharacterPanel[2];
            menuCharacterPanels[0] = new MenuCharacterPanel(new Vector(290, 120), TextureID.MenuLune);
            menuCharacterPanels[1] = new MenuCharacterPanel(new Vector(540, 120), TextureID.MenuSun);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        /// <summary>
        /// メニューが立ち上がる時の処理
        /// </summary>
        public void FadeIn()
        {
            foreach (var panel in menuCharacterPanels)
            {
                panel.FadeIn();
            }
        }

        /// <summary>
        /// メニューが閉じるときの処理
        /// </summary>
        public void FadeOut()
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

        // Input
        public override void OnInputRight() { }
        public override void OnInputLeft() { }
        public override void OnInputUp() { }
        public override void OnInputDown(){ }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
