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
            AddComponents(menuCharacterPanels);
        }

        public int GetIndex() => index;

        // Input
        public override void OnInputRight() {
            index++;
            index = (menuCharacterPanels.Length + index) % menuCharacterPanels.Length;
        }

        public override void OnInputLeft() {
            index--;
            index = (menuCharacterPanels.Length + index) % menuCharacterPanels.Length;
        }
    }
}
