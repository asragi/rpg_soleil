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
        UIFontImage name;
        public MagicCategoryPiece(Vector pos, int tag)
        {
            icon = new UIImage(TextureID.FrameTest, pos, Vector.Zero, DepthID.MenuTop);
            name = new UIFontImage(FontID.Test, pos, null, DepthID.MenuTop);
            name.Text = "陽術";
        }

        public override void Call()
        {
            base.Call();
            icon.Call();
            name.Call();
        }

        public override void Quit()
        {
            base.Quit();
            icon.Quit();
            name.Quit();
        }

        public override void Update()
        {
            base.Update();
            icon.Update();
            name.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            icon.Draw(d);
            name.Draw(d);
        }
    }
}
