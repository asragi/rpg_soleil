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
            valText = new FontImage(FontID.KkBlack, pos, DepthID.MenuTop);
            valText.Text = val.ToString();

            Components = new IComponent[] { valText };
        }
    }
}
