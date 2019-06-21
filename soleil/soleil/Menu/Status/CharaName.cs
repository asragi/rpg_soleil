using Microsoft.Xna.Framework.Graphics;
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
        FontImage nameTitle;

        public CharaName(Vector pos, string text, int length)
        {
            var font = FontID.Yasashisa;
            var color = ColorPalette.DarkBlue;
            double nameDiff = Resources.GetFont(font).MeasureString(text).X;
            name = new FontImage(font, pos + new Vector(length - nameDiff, 0), DepthID.MenuTop);
            name.Text = text;
            name.Color = color;
            nameTitle = new FontImage(FontID.CorpMini, pos + new Vector(0, 7), DepthID.MenuTop);
            nameTitle.Color = color;
            nameTitle.Text = "Name";
            AddComponents(new IComponent[] { nameTitle, name });
        }
    }
}
