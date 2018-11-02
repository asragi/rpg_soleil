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
        Image backImage;
        public StatusSystem(MenuComponent parent)
            : base(parent)
        {
            backImage = new Image(0, Resources.GetTexture(TextureID.MenuBack), Vector.Zero, DepthID.MessageBack, false, true, 0);
            Images = new Image[] { backImage };
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
