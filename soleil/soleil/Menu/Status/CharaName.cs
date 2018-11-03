using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class CharaName : MenuComponent
    {
        FontImage name;

        public CharaName(Vector pos, string text)
        {
            name = new FontImage(FontID.Test, pos, DepthID.MenuTop);
            name.Text = text;
            name.Color = ColorPalette.DarkBlue;
        }

        public override void Call()
        {
            base.Call();
            name.Call();
        }

        public override void Quit()
        {
            base.Quit();
            name.Quit();
        }

        public override void Update()
        {
            base.Update();
            name.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            name.Draw(d);
        }
    }
}
