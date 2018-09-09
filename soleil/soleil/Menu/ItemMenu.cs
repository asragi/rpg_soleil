using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : MenuChild
    {
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {

        }

        public override void OnInputRight() { }
        public override void OnInputLeft() { }
        public override void OnInputUp() { }
        public override void OnInputDown() { }
        public override void OnInputSubmit() { }
        public override void OnInputCancel() { Quit(); }
    }
}
