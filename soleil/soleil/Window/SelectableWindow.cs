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
        public SelectableWindow(Vector _pos, Vector _size,int tag, WindowManager wm, params string[] _options)
            : base(_pos, _size, tag, wm)
        {
            options = _options;
            optionNum = options.Length - 1;
            index = 0;
        }

        protected override void Move()
        {
            // input getkey"press"でとりたい
            if (Keyboard.GetState().IsKeyDown(Keys.Up))index--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))index++;
            index = Math.Min(optionNum,Math.Max(0,index));
        }

        public override void DrawContent(Drawing d)
        {
            for (int i = 0; i < options.Length; i++)
            {
                d.DrawStaticText(pos + new Vector(Spacing, Spacing + LineSpace * i), Resources.GetFont(FontID.Test), options[i], Color.White, DepthID.Frame, Vector.One, 0, false);
            }
            base.DrawContent(d);
        }
    }
}