using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class CharaName : MenuComponent, IPersonUpdate
    {
        readonly FontID MainFont = FontID.CorpM;
        readonly FontID SubFont = FontID.CorpMini;
        readonly Color MainColor = ColorPalette.DarkBlue;
        readonly DepthID depth = DepthID.MenuTop;
        RightAlignText name;
        TextImage nameTitle;

        public CharaName(Vector pos, int length)
        {
            name = new RightAlignText(MainFont, pos + new Vector(length, 0), Vector.Zero, depth);
            name.Color = MainColor;
            nameTitle = new TextImage(SubFont, pos + new Vector(0, 7), depth);
            nameTitle.Text = "Name";
            nameTitle.Color = MainColor;
            AddComponents(new IComponent[] { nameTitle, name });
        }

        public void RefreshWithPerson(Person p)
        {
            name.Text = p.Name.ToString();
        }
    }
}
