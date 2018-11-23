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
        readonly FontID MpFont;
        FontImage valText, slashText, maxText, mpText;
        const int ValSpace = 40;
        const int SpaceToSlash = 50;
        const int SpaceToMax = 20;
        public StatusMP(Vector pos, int val, int max)
        {
            Font = FontID.Touhaba;
            MpFont = FontID.KkGoldMini;
            mpText = new FontImage(MpFont, pos + new Vector(0,8), DepthID.MenuTop);
            mpText.Text = "MP";
            valText = new FontImage(Font, pos + new Vector(ValSpace,0), DepthID.MenuTop);
            valText.Text = val.ToString();
            slashText = new FontImage(Font, pos + new Vector(ValSpace + SpaceToSlash,0), DepthID.MenuTop);
            slashText.Text = "/";
            maxText = new FontImage(Font, pos + new Vector(ValSpace + SpaceToSlash + SpaceToMax, 0), DepthID.MenuTop);
            maxText.Text = max.ToString();
            AddComponents(new IComponent[] { mpText, valText, slashText, maxText });
        }
    }
}
