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
        public int Val { set => val.Text = value.ToString(); }
        public float Alpha { get => text.Alpha; set { text.Alpha = value; val.Alpha = value; } }
        int spacing;

        public TextWithVal(FontID font, Vector pos, int _space, String _text = "", int _val = 0, DepthID depth = DepthID.Message, bool isStatic = true, bool rightAlign = true)
        {
            spacing = _space;
            text = new FontImage(font, pos, depth, isStatic, 0);
            text.Text = _text;
            val = new FontImage(font, pos + new Vector(spacing, 0), depth, isStatic, 0);
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
