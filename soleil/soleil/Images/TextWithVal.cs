﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// 文字とそれに対応する値を持つクラス．
    /// </summary>
    class TextWithVal
    {
        private FontImage text;
        private FontImage val;
        public String Text { get => text.Text; set => text.Text = value; }
        public float Alpha { get => text.Alpha; set { text.Alpha = value; val.Alpha = value; } }
        int spacing;
        bool rightAlign;
        FontID font;

        public int Val
        {
            set
            {
                val.Text = value.ToString();
                // 右揃えにするために文字の幅を取得．
                int rightAlignDiff = rightAlign ? (int)(Resources.GetFont(font).MeasureString(value.ToString()).X) : 0;
                val.Pos = text.Pos + new Vector(spacing - rightAlignDiff, 0);
            }
        }

        public TextWithVal(FontID _font, Vector pos, int _space, String _text = "", int _val = 0, DepthID depth = DepthID.Message, bool isStatic = true, bool _rightAlign = true)
        {
            spacing = _space;
            text = new FontImage(font, pos, depth, isStatic, 0);
            text.Text = _text;
            font = _font;
            rightAlign = _rightAlign;
            
            val = new FontImage(font, Vector.Zero, depth, isStatic, 0);
            Val = _val;
        }

        public void Call()
        {
            text.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            val.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
        }

        public void Quit()
        {
            text.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            val.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
        }

        public void Update()
        {
            text.Update();
            val.Update();
        }

        public void Draw(Drawing d)
        {
            text.Draw(d);
            val.Draw(d);
        }
    }
}
