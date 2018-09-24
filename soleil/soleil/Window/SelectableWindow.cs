using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        const int LineSpace = 35;
        string[] options;
        int optionNum;
        protected int index;
        bool decided;
        public SelectableWindow(Vector _pos, Vector _size, WindowTag tag, WindowManager wm, params string[] _options)
            : base(_pos, _size, tag, wm)
        {
            options = _options;
            optionNum = options.Length - 1;
            index = 0;
            decided = false;
        }

        protected override void Move()
        {

        }

        public void UpCursor()
        {
            if (decided) return;
            index--;
            if (index < 0) index = optionNum;
        }

        public void DownCursor()
        {
            if (decided) return;
            index++;
            if(index > optionNum) index = 0;
        }

        public void Decide()
        {
            decided = true;
        }

        /// <summary>
        /// 決定後であれば選んだ選択肢のindexを返す. 未選択時は常に-1を返す.
        /// </summary>
        /// <returns></returns>
        public int ReturnIndex()
        {
            if (decided) return index;
            return -1;
        }

        public override void DrawContent(Drawing d)
        {
            for (int i = 0; i < options.Length; i++)
            {
                d.DrawStaticText(pos + new Vector(Spacing, Spacing + LineSpace * i), Resources.GetFont(FontID.Test), options[i], Color.White, DepthID.Frame, Vector.One, 0, false);
            }
            d.DrawUI(pos + new Vector(0, 20+Spacing + LineSpace * index), Resources.GetTexture(TextureID.White), DepthID.Frame, 5);
            base.DrawContent(d);
        }
    }
}