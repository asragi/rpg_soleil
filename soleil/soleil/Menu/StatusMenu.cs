using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusMenu : MenuChild
    {
        const int PanelLeft = 290;
        const int PanelRight = 590;
        const int PanelY = 120;

        MenuCharacterPanel[] menuCharacterPanels;
        readonly Func<double, double, double, double, double> EaseFunc = Easing.OutCubic;
        PersonParty party;
        readonly Dictionary<CharaName, TextureID> texDict = new Dictionary<CharaName, TextureID>() {
            {CharaName.Lune, TextureID.MenuLune }, {CharaName.Sunny, TextureID.MenuSun}
        };

        int index;
        public StatusMenu(PersonParty _party, MenuSystem parent)
            :base(parent)
        {
            index = 0;
            menuCharacterPanels = MakePanels(_party);
            AddComponents(menuCharacterPanels);

            MenuCharacterPanel[] MakePanels(PersonParty party)
            {
                var target = party.GetActiveMembers();
                var panels = new MenuCharacterPanel[target.Length];
                int spaceNum = target.Length - 1;
                int space = spaceNum != 0 ? (PanelRight - PanelLeft) / spaceNum : 0;
                for (int i = 0; i < target.Length; i++)
                {
                    panels[i] = new MenuCharacterPanel(new Vector(PanelLeft + space * i, PanelY), texDict[target[i].Name]);
                }
                return panels;
            }
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
