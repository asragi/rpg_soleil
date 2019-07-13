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
        Image backImage;
        // 顔画像
        Image faceImg;
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
        
        public StatusSystem(MenuComponent parent, MenuDescription desc, PersonParty party)
            : base(parent)
        {
            // const
            const int namePosX = 282;
            const int namePosY = 122;
            const int RightX = 550;
            FacePos = new Vector(124, 122);
            NamePos = new Vector(namePosX, namePosY);
            HPPos = new Vector(namePosX, namePosY + 26);
            MPPos = new Vector(namePosX, namePosY + 52);
            ParamsPos = new Vector(namePosX, namePosY + 90);
            EquipPos = new Vector(namePosX, namePosY + 191);
            CategoryPos = new Vector(RightX, namePosY);

            // Component設定
            backImage = new Image(TextureID.MenuBack, Vector.Zero, Vector.Zero, DepthID.MenuMiddle);
            faceImg = new Image(TextureID.MenuLune, FacePos, Vector.Zero, DepthID.MenuMiddle);
            charaName = new CharaName(NamePos, 250);
            display = new HPMPDisplay(HPPos, MPPos);
            statusMagicCategory = new StatusMagicCategory(CategoryPos);
            statusParams = new StatusParamsDisplay(ParamsPos);
            equipDisplay = new EquipDisplay(EquipPos, desc, this);

            //
            AddComponents(new IComponent[] {backImage, charaName, statusParams, display, statusMagicCategory, faceImg });
        }

        public override void OnInputUp()
        {
            base.OnInputUp();
            equipDisplay.OnInputUp();
        }

        public override void OnInputDown()
        {
            base.OnInputDown();
            equipDisplay.OnInputDown();
        }

        public override void OnInputSubmit()
        {
            base.OnInputSubmit();
            equipDisplay.OnInputSubmit();
        }

        public override void OnInputCancel()
        {
            base.OnInputCancel();
            equipDisplay.OnInputCancel();
        }

        public override void Quit()
        {
            base.Quit();
            equipDisplay.Quit();
            ReturnParent();
        }

        public void CallWithPerson(Person p)
        {
            faceImg.TexChange(StatusMenu.GetPersonFaceTexture(p.Name));
            Call();
            equipDisplay.Call(p);
            charaName.RefreshWithPerson(p);
            display.RefreshWithPerson(p);
            statusParams.RefreshWithPerson(p);
        }

        public void Refresh(Person p)
        {
            statusParams.RefreshWithPerson(p);
        }

        public override void Update()
        {
            base.Update();
            equipDisplay.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            equipDisplay.Draw(d);
        }
    }
}
