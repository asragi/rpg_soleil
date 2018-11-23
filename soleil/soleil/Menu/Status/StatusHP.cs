using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusHP : MenuComponent
    {
        FontImage valText, hpText;
        const int ValSpace = 34;
        public StatusHP(Vector pos, int val)
        {
            valText = new FontImage(FontID.Touhaba, pos + new Vector(ValSpace,0), DepthID.MenuTop);
            valText.Text = val.ToString();
            hpText = new FontImage(FontID.KkGoldMini, pos + new Vector(0,8), DepthID.MenuTop);
            hpText.Text = "HP";
            Components = new IComponent[] { hpText,valText };
        }
    }
}
