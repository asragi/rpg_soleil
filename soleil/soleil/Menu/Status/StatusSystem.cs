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
        // 顔画像
        UIImage faceImgs;
        readonly Vector facePos = new Vector(60, 80);
        // おしゃれ移動用参照
        MenuLine[] lines;
        public StatusSystem(MenuComponent parent, params MenuLine[] _lines)
            : base(parent)
        {
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImgs = new UIImage(TextureID.MenuStatusL, facePos, Vector.Zero, DepthID.MenuMiddle);
            
            Images = (new UIImage[] { backImage, faceImgs });
            Components = new MenuComponent[0];
            lines = _lines;
        }

        public override void Call()
        {
            base.Call();
            foreach (var item in lines) item.StartMove(true);
        }

        public override void OnInputCancel()
        {
            base.OnInputCancel();
            foreach (var item in lines) item.StartMove(false);
            Quit();
            ReturnParent();
        }

    }
}
