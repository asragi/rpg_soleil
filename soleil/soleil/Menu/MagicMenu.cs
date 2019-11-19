using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicMenu : MagicMenuBase
    {
        MagicTargetSelect magicTargetSelect;

        //今誰の魔法を選ぼうとしているのか(MagicTargetSelectに渡したい)
        Person currentSelect;

        //複数対象魔法を使うときに渡さないといけないので
        PersonParty party;
        public MagicMenu(MenuComponent parent, MenuDescription desc, PersonParty _party)
            : base(parent, desc)
        {
            party = _party;
        }


        public override void CallWithPerson(Person p)
        {
            currentSelect = p;
            base.CallWithPerson(p);
        }

        public override void OnInputSubmit()
        {
            base.OnInputSubmit();
            var nowPanel = (MagicMenuPanel)Panels[Index];
            magicEffect(nowPanel.ID);

            void magicEffect(SkillID id)
            {
                var temp = (MagicData)SkillDataBase.Get(id);
                if (!temp.OnMenu) return;
                if (temp.TargetRange is Range.Ally)
                {
                    magicTargetSelect.Call();
                    magicTargetSelect.SetWillUsedSkill(id, currentSelect);
                    IsActive = false;
                    Quit();
                }
                else if (temp.TargetRange is Range.AllAlly)
                {
                    SkillEffectData.UseOnMenu(currentSelect, party.GetActiveMembers(), id);
                }
            }
        }

        public void SetRefs(MagicTargetSelect mg, StatusMenu sm)
        {
            magicTargetSelect = mg;
            magicTargetSelect.SetRefs(sm);
        }
    }
}
