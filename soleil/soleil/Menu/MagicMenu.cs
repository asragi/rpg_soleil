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
        MagicIcon[] icons;
        public MagicMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            // Debug
            holder = new SkillHolder();
            holder.LearnSkill(SkillID.MagicalHeal);
            holder.LearnSkill(SkillID.Explode);
            holder.LearnSkill(SkillID.PointFlare);
            categoryToDisplay = MagicCategory.Sun;

            // icon
            icons = new MagicIcon[10];
            for (int i = 0; i < icons.Length; i++)
            {
                var category = (MagicCategory)i;
                var disable = !holder.HasCategory(category);
                icons[i] = new MagicIcon(new Vector(60 + 40 * i, 320), disable, category, this);
            }
            Init();
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
                return;
            }
            InputSide(isRight);
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
                    magList.Add(new MagicMenuPanel(data.Name, i, this));
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
