using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemTargetSelect : StatusTargetSelectBase
    {
        ItemMenu itemMenu;

        public ItemTargetSelect(ItemMenu _parent)
            :base(_parent)
        {
            itemMenu = _parent;
        }

        public override void OnInputSubmit()
        {
            int selected = StatusMenu.GetIndex();
            Console.WriteLine(selected);
        }

        public override void OnInputCancel()
        {
            IsActive = false;
            itemMenu.Call();
        }
    }
}
