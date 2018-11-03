using Soleil.Menu.Status;
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
        readonly Vector FacePos = new Vector(60, 80);
        // 既得術系統
        StatusMagicCategory statusMagicCategory;
        readonly Vector CategoryPos = new Vector(700, 80);
        
        // おしゃれ移動用参照
        MenuLine[] lines;
        public StatusSystem(MenuComponent parent, params MenuLine[] _lines)
            : base(parent)
        {
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImgs = new UIImage(TextureID.MenuStatusL, FacePos, Vector.Zero, DepthID.MenuMiddle);
            statusMagicCategory = new StatusMagicCategory(CategoryPos);

            Images = new [] { backImage, faceImgs };
            Components = new[] { statusMagicCategory };
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
