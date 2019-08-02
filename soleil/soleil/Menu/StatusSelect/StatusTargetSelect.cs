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
        MenuDescription menuDescription;
        string description;
        public StatusTargetSelect(MenuSystem parent, MenuDescription desc, string description)
            : base(parent)
        {
            menuSystem = parent;
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
            Person selected = StatusMenu.GetSelectedPerson();
            IsActive = false;
            menuSystem.CallChild(MenuName.Status, selected);
        }
    }
}
