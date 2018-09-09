using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// メニュー内に存在する入力を受け取る子要素の抽象クラス
    /// </summary>
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

        public void Input()
        {

        }

        public abstract void OnInputRight();
        public abstract void OnInputLeft();
        public abstract void OnInputUp();
        public abstract void OnInputDown();
        public abstract void OnInputSubmit();
        public abstract void OnInputCancel();

    }
}
