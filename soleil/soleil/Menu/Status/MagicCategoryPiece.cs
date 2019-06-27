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
        FontImage name;
        FontImage lv, lvNum;
        public string Name { set => name.Text = value; }
        public MagicCategoryPiece(Vector pos, int tag)
        {
            name = new FontImage(FontID.CorpMini, pos + new Vector(0, MiniHeight / 2), null, DepthID.MenuTop);
            lv = new FontImage(FontID.CorpMini, pos + LvDiff, Vector.Zero, DepthID.MenuTop);
            lvNum = new FontImage(FontID.CormM, pos + LvNumPosDiff, Vector.Zero, DepthID.MenuTop);
            lv.Text = "Lv";
            lvNum.Text = new Random(tag).Next(1, 5).ToString(); // 適当

            name.Color = ColorPalette.DarkBlue;
            lv.Color = ColorPalette.DarkBlue;
            lvNum.Color = ColorPalette.DarkBlue;
            AddComponents(new IComponent[] { lv, lvNum, name });
        }
    }
}
