using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicMenu : BasicMenu
    {
        MagicCategory categoryToDisplay;
        SkillHolder holder;

        // index表示
        const int IconXInitial = 50;
        const int IconXEnd = 360;
        MagicIcon[] icons;
        public MagicMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            // Debug
            holder = new SkillHolder();
            holder.LearnSkill(SkillID.MagicalHeal);
            holder.LearnSkill(SkillID.Explode);
            holder.LearnSkill(SkillID.PointFlare);
            holder.LearnSkill(SkillID.Sonicboom);
            categoryToDisplay = MagicCategory.Sun;

            // icon
            icons = new MagicIcon[10];
            var iconSpace = (IconXEnd - IconXInitial) / (icons.Length - 1);
            for (int i = 0; i < icons.Length; i++)
            {
                var category = (MagicCategory)i;
                var disable = !holder.HasCategory(category);
                icons[i] = new MagicIcon(new Vector(IconXInitial + iconSpace * i, 320), disable, category, this);
            }
            categoryToDisplay = DecideInitialPosition();
            ChangeMagicIconState();
            Init();

            MagicCategory DecideInitialPosition()
            {
                for (int i = 0; i < (int)MagicCategory.size; i++)
                {
                    var c = (MagicCategory)i;
                    if (holder.HasCategory(c)) return c;
                }
                return 0;
            }
        }

        public void InputSide(bool isRight)
        {
            int index = (int)categoryToDisplay;
            int diff = isRight ? 1 : - 1;
            index += diff;
            index += (int)MagicCategory.size;
            index %= (int)MagicCategory.size;
            categoryToDisplay = (MagicCategory)index;
            if (holder.HasCategory(categoryToDisplay))
            {
                Init();
                ChangeMagicIconState();
                return;
            }
            InputSide(isRight);
        }

        private void ChangeMagicIconState()
        {
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].IsSelected = (int)categoryToDisplay == i;
            }
        }

        public override void OnInputRight()
        {
            base.OnInputRight();
            InputSide(true);
            RefreshIndex();
        }

        public override void OnInputLeft()
        {
            base.OnInputLeft();
            InputSide(false);
            RefreshIndex();
        }

        private void RefreshIndex()
        {
            Index = MathEx.Clamp(Index, AllPanels.Length - 1, 0);
            RefreshSelected();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var magList = new List<MagicMenuPanel>();
            for (int i = 0; i < (int)SkillID.size; i++)
            {
                var id = (SkillID)i;
                var _data = SkillDataBase.Get(id);
                if (_data.AttackType != AttackType.Magical) continue;
                var data = (MagicData)_data;
                if (data.Category != categoryToDisplay) continue;
                if (holder.HasSkill(id))
                {
                    magList.Add(new MagicMenuPanel(data, this));
                }
            }
            return magList.ToArray();
        }

        public override void Update()
        {
            base.Update();
            icons.ForEach2(s => s.Update());
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            icons.ForEach2(s => s.Draw(d));
        }
    }
}
