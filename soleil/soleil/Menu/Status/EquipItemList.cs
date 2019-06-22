using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class EquipItemList: BasicMenu
    {
        public bool Active;
        protected override Vector WindowPos => new Vector(550, 100);
        public EquipItemList(MenuComponent parent, MenuDescription desc)
            : base(parent, desc) { Init(); }

        protected override SelectablePanel[] MakeAllPanels()
        {
            return new SelectablePanel[0];
        }

        public override void Call()
        {
            base.Call();
            Active = true;
        }

        public override void Quit()
        {
            base.Quit();
            Active = false;
        }
    }
}
