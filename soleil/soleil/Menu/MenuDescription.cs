using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MenuDescription : MenuComponent
    {
        protected TextImage fontImage;
        public string Text { set { fontImage.Text = value; } }

        public MenuDescription(Vector _pos)
        {
            fontImage = new TextImage(FontID.CorpM, _pos, DepthID.MenuTop);
            fontImage.Color = ColorPalette.DarkBlue;
        }

        public override void Call()
        {
            fontImage.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
        }

        public override void Quit()
        {
            fontImage.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
        }

        public override void Update()
        {
            fontImage.Update();
        }

        public override void Draw(Drawing d)
        {
            fontImage.Draw(d);
        }
    }
}
