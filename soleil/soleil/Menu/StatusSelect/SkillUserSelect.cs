using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class SkillUserSelect: StatusTargetSelectBase
    {
        MenuSystem menuSystem;

        public SkillUserSelect(MenuSystem _ms)
            : base(_ms)
        {
            menuSystem = _ms;
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
