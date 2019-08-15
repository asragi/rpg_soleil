using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MagicTargetSelect : StatusTargetSelectBase
    {
        MagicMenu magicMenu;
        public MagicTargetSelect(MagicMenu mg)
            : base(mg)
        {
            magicMenu = mg;
        }

        public override void OnInputSubmit()
        {
            int selected = StatusMenu.GetIndex();
            Console.WriteLine("Use Magic!");
        }
    }
}
