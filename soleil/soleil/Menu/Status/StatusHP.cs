using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusHP : MenuComponent
    {
        FontImage valText;
        public StatusHP(Vector pos, int val)
        {
            valText = new FontImage(FontID.Test, pos, DepthID.MenuTop);
            valText.Text = val.ToString();
            valText.Color = ColorPalette.DarkBlue;

            AddComponents(new IComponent[] { valText });
        }
    }
}
