using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillUserSelect : StatusTargetSelectBase
    {
        MenuSystem menuSystem;
        MenuDescription menuDescription;
        string description;

        public SkillUserSelect(MenuSystem _ms, MenuDescription desc, string description)
            : base(_ms)
        {
            menuSystem = _ms;
            menuDescription = desc;
            this.description = description;
        }

        protected override void OnEnable()
        {
            menuDescription.Text = description;
            base.OnEnable();
        }

        public override void OnInputSubmit()
        {
            base.OnInputSubmit();
            Person selected = StatusMenu.GetSelectedPerson();
            IsActive = false;
            menuSystem.CallChild(MenuName.Skill, selected);
        }
    }
}
