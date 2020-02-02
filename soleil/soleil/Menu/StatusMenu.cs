using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusMenu : MenuChild, IListener
    {
        const int PanelLeft = 368;
        const int PanelRight = 696;
        const int PanelDiffFor2 = 210;
        const int PanelY = 130;

        MenuCharacterPanel[] menuCharacterPanels;
        static readonly Dictionary<CharaName, TextureID> texDict = new Dictionary<CharaName, TextureID>() {
            {CharaName.Lune, TextureID.MenuLune }, {CharaName.Sunny, TextureID.MenuSun},
            {CharaName.Tella, TextureID.MenuTella }
        };

        PersonParty party;

        MenuCursor cursor;
        int index;

        public StatusMenu(PersonParty _party, MenuSystem parent)
            : base(parent)
        {
            index = 0;
            party = _party;
            party.AddListener(this);
            CreatePanels();
        }

        ListenerType IListener.Type => ListenerType.StatusMenu;

        public int GetIndex() => index;
        public Person GetSelectedPerson() => party.GetActiveMembers()[index];

        public void CallCursor() => cursor.Call();
        public void QuitCursor() => cursor.Quit();

        // Input
        public override void OnInputRight()
        {
            index++;
            AdjustIndex();
        }

        public override void OnInputLeft()
        {
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

        public static TextureID GetPersonFaceTexture(CharaName name)
        {
            return texDict[name];
        }

        private void CreatePanels()
        {
            var people = party.GetActiveMembers();
            int size = people.Length;
            Vector[] positions = Positions(size);
            menuCharacterPanels = MakePanels(people, positions);
            cursor = new MenuCursor(TextureID.MenuStatusCursor, positions);
            Clear();
            AddComponents(menuCharacterPanels);

            Vector[] Positions(int num)
            {
                if (num == 1)
                    return new[] { new Vector((PanelRight + PanelLeft) / 2, PanelY) };
                var result = new Vector[num];
                if (num == 2)
                {
                    int center = (PanelLeft + PanelRight) / 2;
                    for (int i = 0; i < num; i++)
                    {
                        result[i] = new Vector(center + PanelDiffFor2 / 2 * Math.Pow((-1), i - 1), PanelY);
                    }
                    return result;
                }
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

        void IListener.OnListen(INotifier i)
        {
            if (!(i is PersonParty)) return;
            CreatePanels();
        }
    }
}
