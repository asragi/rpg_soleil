using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil.Battle
{

    ///<summary>
    /// 攻撃対象を選択する矢印
    ///</summary>
    class CharaSelectWindow : MenuChild
    {
        Reference<bool> selectCompleted;
        public int SelectIndex { get; private set; }
        List<int> charaindexList;

        int index;

        MenuDescription menuDescription; //未使用 MagicSelectWindowと合わせるため一応保持する
        MagicSelectWindow parent;
        public CharaSelectWindow(MagicSelectWindow parent, MenuDescription desc, List<int> charaindexList, Reference<bool> selectCompleted)
            : base(parent)
        {
            this.selectCompleted = selectCompleted;
            this.charaindexList = charaindexList;
            this.parent = parent;
            menuDescription = desc;
        }

        public override void Update()
        {
            base.Update();
        }

        public void UpdateInput()
        {
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
        }

        public override void Draw(Drawing d)
        {
            if (IsActive)
            {
                base.Draw(d);
                var pos = BattleField.GetInstance().GetCharacter(charaindexList[index]).BCGraphics.Pos;
                d.Draw(pos + new Vector(0, -50), Resources.GetTexture(TextureID.Triangle), DepthID.MenuMessage, 2, (float)Math.PI);
            }
        }

        public override void OnInputUp()
        {
            index = (index - 1 + charaindexList.Count) % charaindexList.Count;
        }

        public override void OnInputDown()
        {
            index = (index + 1 + charaindexList.Count) % charaindexList.Count;
        }

        public override void OnInputSubmit()
        {
            SelectIndex = charaindexList[index];
            selectCompleted.Val = true;
        }

        public override void OnInputCancel() { Quit(); ReturnParent(); }

        protected override void ReturnParent()
        {
            base.ReturnParent();
            parent.Activate();
        }

        public override void Quit()
        {
            if (IsActive)
                base.Quit();
            IsActive = false;
        }
    }
}
