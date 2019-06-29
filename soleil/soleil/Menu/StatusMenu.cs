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
        const int PanelLeft = 368;
        const int PanelRight = 696;
        const int PanelY = 130;

        MenuCharacterPanel[] menuCharacterPanels;
        readonly Dictionary<CharaName, TextureID> texDict = new Dictionary<CharaName, TextureID>() {
            {CharaName.Lune, TextureID.MenuLune }, {CharaName.Sunny, TextureID.MenuSun},
            {CharaName.Tella, TextureID.MenuTella }
        };

        PersonParty party;

        MenuCursor cursor;
        int index;

        public StatusMenu(PersonParty _party, MenuSystem parent)
            :base(parent)
        {
            index = 0;
            party = _party;
            var people = _party.GetActiveMembers();
            int size = people.Length;
            Vector[] positions = Positions(size);
            cursor = new MenuCursor(TextureID.MenuStatusCursor, positions);
            menuCharacterPanels = MakePanels(people, positions);
            AddComponents(menuCharacterPanels);

            Vector[] Positions(int num)
            {
                var result = new Vector[num];
                int spaceNum = num - 1;
                int space = spaceNum != 0 ? (PanelRight - PanelLeft) / spaceNum : 0;
                for (int i = 0; i < num; i++)
                {
                    result[i] = new Vector(PanelLeft + space * i, PanelY);
                }
                return result;
            }

            MenuCharacterPanel[] MakePanels(Person[] _people, Vector[] pos)
            {
                var target = _people;
                var panels = new MenuCharacterPanel[target.Length];
                for (int i = 0; i < target.Length; i++)
                {
                    panels[i] = new MenuCharacterPanel(target[i], pos[i], texDict[target[i].Name]);
                    panels[i].FrameWait = 5 * i;
                }
                return panels;
            }
        }

        public int GetIndex() => index;
        public Person GetSelectedPerson() => party.GetActiveMembers()[index];

        public void CallCursor() => cursor.Call();
        public void QuitCursor() => cursor.Quit();

        // Input
        public override void OnInputRight() {
            index++;
            AdjustIndex();
        }

        public override void OnInputLeft() {
            index--;
            AdjustIndex();
        }

        private void AdjustIndex()
        {
            index = (menuCharacterPanels.Length + index) % menuCharacterPanels.Length;
            cursor.MoveTo(index);
        }

        public override void Update()
        {
            cursor.Update();
            base.Update();
        }

        public override void Draw(Drawing d)
        {
            cursor.Draw(d);
            base.Draw(d);
        }
    }
}
