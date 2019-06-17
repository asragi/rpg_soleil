using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillMenu: BasicMenu
    {
        SkillHolder skillHolder;
        public SkillMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            skillHolder = new SkillHolder();
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var skillList = new List<SkillMenuPanel>();
            for (int i = 0; i < (int)SkillID.size; i++)
            {
                var id = (SkillID)i;
                var _data = SkillDataBase.Get(id);
                if (_data.AttackType != AttackType.Physical) continue;
                if (!skillHolder.HasSkill(id)) continue;
                skillList.Add(new SkillMenuPanel(_data, this));
            }
            return skillList.ToArray();
        }
    }
}
