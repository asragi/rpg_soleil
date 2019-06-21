using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class MagicCategoryPiece : MenuComponent
    {
        readonly Vector Space = new Vector(10, 10);
        readonly Vector LvDiff = new Vector(176, 0);
        readonly Vector LvNumPosDiff = new Vector(23, 0);
        FontImage name;
        FontImage lv, lvNum;
        public string Name { set => name.Text = value; }
        public MagicCategoryPiece(Vector pos, int tag)
        {
            name = new FontImage(FontID.Yasashisa, pos + Space, null, DepthID.MenuTop);
            lv = new FontImage(FontID.Yasashisa, pos + Space + LvDiff, Vector.Zero, DepthID.MenuTop);
            lvNum = new FontImage(FontID.Yasashisa, pos + Space + LvDiff + LvNumPosDiff, Vector.Zero, DepthID.MenuTop);
            lv.Text = "Lv";
            lvNum.Text = new Random(tag).Next(1, 5).ToString(); // 適当

            name.Color = ColorPalette.DarkBlue;
            lv.Color = ColorPalette.DarkBlue;
            lvNum.Color = ColorPalette.DarkBlue;
            AddComponents(new IComponent[] { lv, lvNum, name });
        }
    }
}
