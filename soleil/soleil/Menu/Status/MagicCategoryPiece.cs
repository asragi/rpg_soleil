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
        UIImage icon;
        FontImage name;
        public string Name { set => name.Text = value; }
        public MagicCategoryPiece(Vector pos, int tag)
        {
            icon = new UIImage(TextureID.FrameTest, pos, Vector.Zero, DepthID.MenuTop);
            name = new FontImage(FontID.KkMini, pos + Space, null, DepthID.MenuTop);
            name.Text = "陽術";
            Components = new IComponent[] { icon, name };
        }
    }
}
