using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class MagicCategoryPiece : MenuComponent
    {
        UIImage icon;
        public MagicCategoryPiece(Vector pos, int tag)
        {
            icon = new UIImage(TextureID.FrameTest, pos, Vector.Zero, DepthID.MenuTop);
        }

        public override void Call()
        {
            base.Call();
            icon.Call();
        }

        public override void Quit()
        {
            base.Quit();
            icon.Quit();
        }

        public override void Update()
        {
            base.Update();
            icon.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            icon.Draw(d);
        }
    }
}
