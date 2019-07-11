using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class StatusHPMP : MenuComponent
    {
        readonly FontID Font;
        readonly FontID MpFont;
        TextImage  slashText, mpText;
        RightAlignText valText, maxText;
        const int ValSpace = 170;
        const int SpaceToSlash = 10;
        const int SpaceToMax = 70;
        public StatusHPMP(Vector pos)
        {
            var color = ColorPalette.DarkBlue;
            Font = FontID.CorpM;
            MpFont = FontID.CorpMini;
            mpText = new TextImage(MpFont, pos + new Vector(0,7), DepthID.MenuTop);
            mpText.Text = "MP";
            mpText.Color = color;
            valText = new RightAlignText(Font, pos + new Vector(ValSpace, 0), Vector.Zero, DepthID.MenuTop);
            valText.Color = color;
            slashText = new TextImage(Font, pos + new Vector(ValSpace + SpaceToSlash,0), DepthID.MenuTop);
            slashText.Text = "/";
            slashText.Color = color;
            maxText = new RightAlignText(Font, pos + new Vector(ValSpace + SpaceToSlash + SpaceToMax, 0), Vector.Zero, DepthID.MenuTop);
            maxText.Color = color;
            AddComponents(new IComponent[] { mpText, valText, slashText, maxText });
        }

        public void RefreshWithPerson(Person p, bool isHP)
        {
            valText.Text = isHP ? p.Score.HP.ToString() : p.Score.MP.ToString();
            maxText.Text = isHP ? p.Score.HPMAX.ToString() : p.Score.MPMAX.ToString();
        }
    }
}
