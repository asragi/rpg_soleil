using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusMP : MenuComponent
    {
        readonly FontID Font;
        FontImage valText, slashText, maxText;
        const int SpaceToSlash = 70;
        const int SpaceToMax = 30;
        public StatusMP(Vector pos, int val, int max)
        {
            Font = FontID.KkBlack;
            valText = new FontImage(Font, pos, DepthID.MenuTop);
            valText.Text = val.ToString();
            slashText = new FontImage(Font, pos + new Vector(SpaceToSlash,0), DepthID.MenuTop);
            slashText.Text = "/";
            maxText = new FontImage(Font, pos + new Vector(SpaceToSlash + SpaceToMax, 0), DepthID.MenuTop);
            maxText.Text = max.ToString();
            Components = new IComponent[] { valText, slashText, maxText };
        }
    }
}
