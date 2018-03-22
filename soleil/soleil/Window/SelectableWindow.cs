using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// カーソルを用いた選択を行う機能を持つウィンドウ
    /// </summary>
    class SelectableWindow
        :Window
    {
        public SelectableWindow(Vector _pos, Vector _size, WindowManager wm)
            : base(_pos, _size, wm)
        {

        }
    }
}
