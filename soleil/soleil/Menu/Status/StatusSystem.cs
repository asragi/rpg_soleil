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
        // 装備
        EquipDisplay equipDisplay;
        readonly Vector EquipPos;
        // 既得術系統
        StatusMagicCategory statusMagicCategory;
        readonly Vector CategoryPos;
        
        public StatusSystem(MenuComponent parent)
            : base(parent)
        {
            // const
            const int namePosX = 282;
            const int namePosY = 128;
            const int RightX = 550;
            FacePos = new Vector(124, 122);
            NamePos = new Vector(namePosX, namePosY);
            HPPos = new Vector(namePosX, namePosY + 26);
            MPPos = new Vector(namePosX, namePosY + 52);
            ParamsPos = new Vector(namePosX, 207);
            EquipPos = new Vector(namePosX, 319);
            CategoryPos = new Vector(RightX, namePosY - 6);

            // Component設定
            backImage = new UIImage(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImg = new UIImage(TextureID.MenuLune, FacePos, Vector.Zero, DepthID.MenuMiddle);
            charaName = new CharaName(NamePos, "ルーネ");
            display = new HPMPDisplay(HPPos, 368, MPPos, 642, 765);
            statusMagicCategory = new StatusMagicCategory(CategoryPos);
            statusParams = new StatusParamsDisplay(ParamsPos);
            equipDisplay = new EquipDisplay(EquipPos);

            //
            AddComponents(new IComponent[] {backImage, charaName, statusParams, display, equipDisplay, statusMagicCategory, faceImg });
            
        }

        public override void OnInputCancel()
        {
            base.OnInputCancel();
            Quit();
            ReturnParent();
        }

    }
}
