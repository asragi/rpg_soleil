using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillMenu : BasicMenu
    {
        protected SkillHolder SHolder;
        public SkillMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            SHolder = new SkillHolder();
            Init();
        }

        public void SetPerson(Person p)
        {
            SHolder = p.Skill;
            Init();
        }

        public void CallWithPerson(Person p)
        {
            SetPerson(p);
            Call();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            //onMenuがtrueのものを上側に置く
            var availableSkillList = new List<SkillMenuPanel>();
            var unavailableSkillList = new List<SkillMenuPanel>();
            for (int i = 0; i < (int)SkillID.size; i++)
            {
                var id = (SkillID)i;
                var _data = SkillDataBase.Get(id);
                if (_data.AttackType != AttackType.Physical) continue;
                if (!SHolder.HasSkill(id)) continue;
                if (_data.OnMenu) availableSkillList.Add(new SkillMenuPanel(_data, this, true));
                else unavailableSkillList.Add(new SkillMenuPanel(_data, this, false));
            }
            var skillArray = new SelectablePanel[availableSkillList.Count() + unavailableSkillList.Count()];
            availableSkillList.ToArray().CopyTo(skillArray, 0);
            unavailableSkillList.ToArray().CopyTo(skillArray, availableSkillList.Count());
            return skillArray;
        }
    }
}
