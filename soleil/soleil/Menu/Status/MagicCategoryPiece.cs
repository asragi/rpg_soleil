using Microsoft.Xna.Framework;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class MagicCategoryPiece : EasingComponent
    {
        const int MiniHeight = 7;
        readonly Vector LvCircleDiff = new Vector(35, 3);
        readonly Vector LvCircleSize = new Vector(15, 0);
        readonly Vector LvDiff = new Vector(176, MiniHeight);
        readonly Vector LvNumPosDiff = new Vector(200, 0);
        public readonly MagicCategory category;
        TextImage name;
        TextImage lv, lvNum;
        TextImage[] lvCircle;
        public string Name { set => name.Text = value; }
        public int Lv
        {
            set
            {
                lvNum.Text = value.ToString();
                for (int i = 0; i < 9; ++i)
                {
                    lvCircle[i].IsVisible = value > i;
                }
            }
        }
        public Color Color
        {
            set { name.Color = value; lv.Color = value; lvNum.Color = value; }
        }
        public MagicCategoryPiece(Vector pos, MagicCategory tag)
        {
            category = tag;
            name = new TextImage(FontID.CorpMini, pos + new Vector(0, MiniHeight / 2), DepthID.MenuTop);
            lv = new TextImage(FontID.CorpMini, pos + LvDiff, Vector.Zero, DepthID.MenuTop);
            lvNum = new TextImage(FontID.CorpM, pos + LvNumPosDiff, Vector.Zero, DepthID.MenuTop);
            lvCircle = new TextImage[9];
            for (int i = 0; i < 9; ++i)
            {
                Vector lvcpos = pos + LvCircleDiff;
                TextImage circle =
                    new TextImage(FontID.CorpMini, lvcpos + LvCircleSize * i, Vector.Zero, DepthID.MenuTop)
                    {
                        Text = "〇",
                        Color = ColorPalette.MagicColors[tag]
                    };
                lvCircle[i] = circle;
            }
            lv.Text = "Lv";

            name.Color = ColorPalette.DarkBlue;
            lv.Color = ColorPalette.DarkBlue;
            lvNum.Color = ColorPalette.DarkBlue;
            AddComponents(new IComponent[] { lv, lvNum, name });
            AddComponents(lvCircle);
        }
    }
}
