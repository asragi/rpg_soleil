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
        readonly Vector LvDiff = new Vector(4, 36);
        readonly Vector LvNumPosDiff = new Vector(34, -5);
        UIImage icon;
        FontImage name;
        FontImage lv, lvNum;
        public string Name { set => name.Text = value; }
        public MagicCategoryPiece(Vector pos, int tag)
        {
            // icon = new UIImage(TextureID.FrameTest, pos, Vector.Zero, DepthID.MenuTop);
            name = new FontImage(FontID.CorpM, pos + Space, null, DepthID.MenuTop);
            lv = new FontImage(FontID.CorpM, pos + Space + LvDiff, Vector.Zero, DepthID.MenuTop);
            lvNum = new FontImage(FontID.CorpM, pos + Space + LvDiff + LvNumPosDiff, Vector.Zero, DepthID.MenuTop);
            lv.Text = "Lv.";
            lvNum.Text = new Random(tag).Next(1, 5).ToString(); // 適当
            AddComponents(new IComponent[] { lv, lvNum, name });
        }
    }
}
