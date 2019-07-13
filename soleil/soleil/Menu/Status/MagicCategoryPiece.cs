using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class MagicCategoryPiece : MenuComponent
    {
        const int MiniHeight = 7;
        readonly Vector LvDiff = new Vector(176, MiniHeight);
        readonly Vector LvNumPosDiff = new Vector(200, 0);
        public readonly MagicCategory category = MagicCategory.None;
        TextImage name;
        TextImage lv, lvNum;
        public string Name { set => name.Text = value; }
        public string Lv { set => lvNum.Text = value; }
        public MagicCategoryPiece(Vector pos, MagicCategory tag)
        {
            category = tag;
            name = new TextImage(FontID.CorpMini, pos + new Vector(0, MiniHeight / 2), DepthID.MenuTop);
            lv = new TextImage(FontID.CorpMini, pos + LvDiff, Vector.Zero, DepthID.MenuTop);
            lvNum = new TextImage(FontID.CorpM, pos + LvNumPosDiff, Vector.Zero, DepthID.MenuTop);
            lv.Text = "Lv";
            lvNum.Text = 0.ToString(); // 適当

            name.Color = ColorPalette.DarkBlue;
            lv.Color = ColorPalette.DarkBlue;
            lvNum.Color = ColorPalette.DarkBlue;
            AddComponents(new IComponent[] { lv, lvNum, name });
        }
    }
}
