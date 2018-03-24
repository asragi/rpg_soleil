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
        string[] options;
        int optionNum;
        protected int index;
        public SelectableWindow(Vector _pos, Vector _size, WindowManager wm, params string[] _options)
            : base(_pos, _size, wm)
        {
            options = _options;
            optionNum = options.Length;
            index = 0;
        }

        protected override void Move()
        {
            Console.WriteLine("hoge");
        }

        public override void DrawContent(Drawing d)
        {
            base.DrawContent(d);
        }
    }
}
