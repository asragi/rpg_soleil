using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicUserSelect : StatusTargetSelectBase
    {
        MenuSystem menuSystem;
        MenuDescription menuDescription;
        string description;
        public MagicUserSelect(MenuSystem _menuSystem, MenuDescription desc, string description)
            : base(_menuSystem)
        {
            menuSystem = _menuSystem;
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
            Audio.PlaySound(SoundID.DecideSoft);
            menuSystem.CallChild(MenuName.Magic, selected);
        }
    }
}
