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
            Components = new IComponent[] { name };
        }
    }
}
