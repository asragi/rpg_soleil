using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class MenuCursor: MenuComponent
    {
        UIImage img;
        Vector[] positions;
        public MenuCursor(TextureID id, Vector[] destinations)
        {
            positions = destinations;
            img = new UIImage(id, destinations[0], Vector.Zero, DepthID.MenuBottom);
            AddComponents(new[] { img });
        }

        public void MoveTo(int index)
        {
            img.MoveTo(positions[index], img.FadeSpeed, MenuSystem.EaseFunc);
        }
    }
}
