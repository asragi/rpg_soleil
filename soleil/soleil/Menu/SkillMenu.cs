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
        SkillHolder skillHolder;
        public SkillMenu(MenuComponent parent, MenuDescription desc)
            : base(parent, desc)
        {
            skillHolder = new SkillHolder();
            Init();
        }

        public void CallWithPerson(Person p)
        {
            skillHolder = p.Skill;
            Init();
            Call();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var availableSkillList = new List<SkillMenuPanel>();
            var unavailableSkillList = new List<SkillMenuPanel>();
            for (int i = 0; i < (int)SkillID.size; i++)
            {
                var id = (SkillID)i;
                var _data = SkillDataBase.Get(id);
                if (_data.AttackType != AttackType.Physical) continue;
                var ele = new SkillMenuPanel(_data, this);
                if (skillHolder.HasSkill(id)) availableSkillList.Add(ele);
                else
                {
                    ele.Active = false;
                    ele.Update();
                    unavailableSkillList.Add(ele);
                }
                //if (!skillHolder.HasSkill(id)) continue;
                //skillList.Add(new SkillMenuPanel(_data, this));
            }
            var avSkillArray = availableSkillList.ToArray();
            var unavSkillArray = unavailableSkillList.ToArray();
            var skillArray = new SelectablePanel[avSkillArray.Count() + unavSkillArray.Count()];
            avSkillArray.CopyTo(skillArray, 0);
            unavSkillArray.CopyTo(skillArray, avSkillArray.Count());
            return skillArray;
            //return skillList.ToArray();
        }
    }
}
