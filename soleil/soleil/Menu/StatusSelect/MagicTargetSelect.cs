using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Skill;

namespace Soleil.Menu
{
    class MagicTargetSelect : StatusTargetSelectBase
    {
        //その魔法を唱える人
        Person Commander;
        MagicMenu magicMenu;
        // 使用情報
        SkillID id;
        public MagicTargetSelect(MagicMenu mg)
            : base(mg)
        {
            magicMenu = mg;
        }
        public void SetWillUsedSkill(SkillID _id,Person com)
        {
            id = _id;
            Commander = com;
        }
        public override void OnInputSubmit()
        {
            Person selectedPerson = StatusMenu.GetSelectedPerson();
            SkillEffectData.UseOnMenu(Commander,selectedPerson,id);
        }
        public override void OnInputCancel()
        {
            base.OnInputCancel();
            magicMenu.Call();
        }
    }
}
