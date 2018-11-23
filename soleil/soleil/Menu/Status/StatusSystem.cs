using Soleil.Menu.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class StatusSystem: MenuChild
    {
        // 背景画像
        UIImage backImage;
        // 顔画像
        UIImage faceImg;
        readonly Vector FacePos;
        // 名前
        CharaName charaName;
        readonly Vector NamePos;
        // HPMP 表示
        HPMPDisplay display;
        readonly Vector HPPos;
        readonly Vector MPPos;
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
            const int namePosX = 350;
            const int namePosY = 80;
            FacePos = new Vector(60, 80);
            NamePos = new Vector(namePosX, namePosY);
            HPPos = new Vector(500, namePosY);
            MPPos = new Vector(namePosX + 49, namePosY + 40);
            ParamsPos = new Vector(namePosX, 240);
            CategoryPos = new Vector(700, 80);

            // Component設定
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImg = new UIImage(TextureID.MenuStatusL, FacePos, Vector.Zero, DepthID.MenuMiddle);
            charaName = new CharaName(NamePos, "ルーネ");
            display = new HPMPDisplay(HPPos, 368, MPPos, 642, 765);
            statusMagicCategory = new StatusMagicCategory(CategoryPos);
            statusParams = new StatusParamsDisplay(ParamsPos);

            //
            AddComponents(new IComponent[] {backImage, charaName, statusParams, display, statusMagicCategory, faceImg });
            
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
