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
            name = new FontImage(FontID.KkBlack, pos, DepthID.MenuTop);
            name.Text = text;
            Components = new IComponent[] { name };
        }
    }
}
