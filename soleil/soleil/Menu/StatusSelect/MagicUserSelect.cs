using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicUserSelect: StatusTargetSelectBase
    {
        MenuSystem menuSystem;
        public MagicUserSelect(MenuSystem _menuSystem)
            :base(_menuSystem)
        {
            menuSystem = _menuSystem;
        }

        public override void OnInputSubmit()
        {
            Person selected = StatusMenu.GetSelectedPerson();
            IsActive = false;
            menuSystem.CallChild(MenuName.Magic, selected);
        }
    }
}
