﻿using Microsoft.Xna.Framework;
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
        readonly FontID MainFont = FontID.Yasashisa;
        readonly FontID SubFont = FontID.CorpMini;
        readonly Color MainColor = ColorPalette.DarkBlue;
        readonly DepthID depth = DepthID.MenuTop;
        RightAlignText name;
        FontImage nameTitle;

        public CharaName(Vector pos, int length)
        {
            name = new RightAlignText(MainFont, pos + new Vector(length, 0), Vector.Zero, depth);
            name.Color = MainColor;
            nameTitle = new FontImage(SubFont, pos + new Vector(0, 7), depth);
            AddComponents(new IComponent[] { nameTitle, name });
        }

        public void RefreshWithPerson(Person p)
        {
            name.Text = p.Name.ToString();
        }
    }
}
