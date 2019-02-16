using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemTargetSelect : MenuChild
    {
        StatusMenu statusMenu;
        ItemMenu itemMenu;

        public ItemTargetSelect(ItemMenu _parent)
            :base(_parent)
        {
            itemMenu = _parent;
        }

        public void SetRefs(StatusMenu sm) => statusMenu = sm;

        public override void OnInputRight() => statusMenu.OnInputRight();

        public override void OnInputLeft() => statusMenu.OnInputLeft();

        public override void OnInputSubmit()
        {
            int selected = statusMenu.GetIndex();
            Console.WriteLine(selected);
        }

        public override void OnInputCancel()
        {
            IsActive = false;
            itemMenu.Call();
        }
    }
}
