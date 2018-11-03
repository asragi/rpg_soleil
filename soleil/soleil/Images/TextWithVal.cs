using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    class TextWithVal : IComponent
    {
        private FontImage text;
        private FontImage val;
        public Color TextColor { set => text.Color = value; }
        public Color ValColor { set => val.Color = value; }
        public String Text { get => text.Text; set => text.Text = value; }
        public float Alpha { get => text.Alpha; set { text.Alpha = value; val.Alpha = value; } }
        int spacing;
        bool rightAlign, underAlign;
        FontID font;
        public FontID Font { set { font = value; text.Font = font; } }
        FontID valFont;
        public FontID ValFont { set { valFont = value; val.Font = valFont; val.Pos = AlignPos(); } }
        public bool Enable = true;
        public bool EnableValDisplay = true;

        public Vector Pos
        {
            get => text.Pos;
            set
            {
                text.Pos = value;
                val.Pos = AlignPos();
            }
        }

        public int Val
        {
            set
            {
                val.Text = value.ToString();
                val.Pos = AlignPos();
            }
        }

        // 右揃えにするために文字の幅を取得し，適切な位置を返す．
        private Vector AlignPos()
        {
            int rightAlignDiff = rightAlign ? (int)(Resources.GetFont(valFont).MeasureString(val.Text).X) : 0;
            int underAlignDiff = underAlign ? (int)Resources.GetFont(font).MeasureString(text.Text).Y - (int)(Resources.GetFont(valFont).MeasureString(val.Text).Y) : 0;
            return text.Pos + new Vector(spacing - rightAlignDiff, underAlignDiff);
        }

        public TextWithVal(FontID _font, Vector pos, int _space, String _text = "", int _val = 0, DepthID depth = DepthID.Message, bool isStatic = true, bool _rightAlign = true, bool _underAlign = true)
        {
            spacing = _space;
            font = _font;
            valFont = _font;
            text = new FontImage(font, pos, depth, isStatic, 0);
            text.Text = _text;
            (rightAlign, underAlign) = (_rightAlign, _underAlign);
            
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
            if (!Enable) return;
            text.Draw(d);
            if (!EnableValDisplay) return;
            val.Draw(d);
        }
    }
}
