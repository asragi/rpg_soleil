using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusSystem: SystemBase
    {
        // 背景画像
        UIImage backImage;
        public StatusSystem(MenuComponent parent)
            : base(parent)
        {
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            Images = new UIImage[] { backImage };
            Components = new MenuComponent[0];
        }

        public override void OnInputCancel()
        {
            base.OnInputCancel();
            Quit();
            ReturnParent();
        }

    }
}
