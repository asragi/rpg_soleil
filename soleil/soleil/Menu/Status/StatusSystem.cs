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
        readonly Vector FacePos;
        // 名前
        CharaName charaName;
        readonly Vector NamePos;
        // ステータスパラメータ
        StatusParamsDisplay statusParams;
        readonly Vector ParamsPos;
        // 既得術系統
        StatusMagicCategory statusMagicCategory;
        readonly Vector CategoryPos;
        
        // おしゃれ移動用参照
        MenuLine[] lines;
        public StatusSystem(MenuComponent parent, params MenuLine[] _lines)
            : base(parent)
        {
            // const
            const int namePos = 350;
            FacePos = new Vector(60, 80);
            NamePos = new Vector(namePos, 80);
            ParamsPos = new Vector(namePos, 240);
            CategoryPos = new Vector(700, 80);

            // Component設定
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImgs = new UIImage(TextureID.MenuStatusL, FacePos, Vector.Zero, DepthID.MenuMiddle);
            charaName = new CharaName(NamePos, "ルーネ");
            statusMagicCategory = new StatusMagicCategory(CategoryPos);
            statusParams = new StatusParamsDisplay(ParamsPos);

            //
            Images = new [] { backImage, faceImgs };
            Components = new MenuComponent[] { charaName, statusParams, statusMagicCategory };
            
            //
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
