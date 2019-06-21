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
        const int ValSpace = 131;
        const int SpaceToSlash = 50;
        const int SpaceToMax = 20;
        public StatusMP(Vector pos, int val, int max)
        {
            var color = ColorPalette.DarkBlue;
            Font = FontID.Yasashisa;
            MpFont = FontID.CorpMini;
            mpText = new FontImage(MpFont, pos + new Vector(0,7), DepthID.MenuTop);
            mpText.Text = "MP";
            mpText.Color = color;
            valText = new FontImage(Font, pos + new Vector(ValSpace, 0), DepthID.MenuTop);
            valText.Text = val.ToString();
            valText.Color = color;
            slashText = new FontImage(Font, pos + new Vector(ValSpace + SpaceToSlash,0), DepthID.MenuTop);
            slashText.Text = "/";
            slashText.Color = color;
            maxText = new FontImage(Font, pos + new Vector(ValSpace + SpaceToSlash + SpaceToMax, 0), DepthID.MenuTop);
            maxText.Text = max.ToString();
            maxText.Color = color;
            AddComponents(new IComponent[] { mpText, valText, slashText, maxText });
        }
    }
}
