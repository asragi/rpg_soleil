using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MenuDescription
    {
        FontImage fontImage;
        public String Text { set { fontImage.Text = value; } }

        public MenuDescription(Vector _pos)
        {
            fontImage = new FontImage(FontID.Test, _pos, DepthID.Message, true, 1);
            fontImage.Color = ColorPalette.DarkBlue;
            fontImage.EnableShadow = false;
            fontImage.ShadowPos = new Vector(3, 3);
            fontImage.ShadowColor = ColorPalette.GlayBlue;
        }

        public void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            fontImage.Fade(duration, _easeFunc, isFadeIn);
        }

        public void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            fontImage.MoveTo(target, duration, _easeFunc);
        }

        public void Update()
        {
            fontImage.Update();
        }

        public void Draw(Drawing d)
        {
            fontImage.Draw(d);
        }
    }
}
