using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    abstract class MenuChild : MenuComponent
    {
        MenuComponent parent;
        public MenuChild(MenuComponent _parent)
        {
            parent = _parent;
        }

        /// <summary>
        /// ウィンドウを閉じ親にActiveを戻す
        /// </summary>
        protected virtual void Quit()
        {
            parent.IsActive = true;
            IsActive = false;
        }
    }
}
