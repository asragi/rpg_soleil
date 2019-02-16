using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusTargetSelect : StatusTargetSelectBase
    {
        MenuSystem menuSystem;
        public StatusTargetSelect(MenuSystem parent)
            : base(parent)
        {
            menuSystem = parent;
        }

        public override void OnInputSubmit()
        {
            int selected = StatusMenu.GetIndex();
            IsActive = false;
            menuSystem.CallChild(MenuName.Status);
        }

        public override void OnInputCancel()
        {
            IsActive = false;
            menuSystem.IsActive = true;
        }
    }
}
