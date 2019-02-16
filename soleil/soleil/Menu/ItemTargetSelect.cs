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

        public ItemTargetSelect(ItemMenu _parent)
            :base(_parent){ }

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
            Console.WriteLine("CANCEL");
            ReturnParent();
        }
    }
}
