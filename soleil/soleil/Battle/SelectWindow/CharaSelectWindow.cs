using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil
{
    /// <summary>
    /// CharaIndexWindwosで使う
    /// </summary>
    class CharaIndexPanel : TextSelectablePanel
    {
        public readonly Vector CostPosDiff = new Vector(300, 0);
        public override string Desctiption => ItemName;

        public CharaIndexPanel(String index, int num, CharaSelectWindow parent)
            : base(index, parent)
        {
            // itemNum
            Val = num;
            LocalPos = Vector.Zero;
        }
    }
    /*
     * 暫定実装
     * 多分矢印で選択とかになるのでは
     */
    class CharaSelectWindow : BasicMenu
    {
        Reference<bool> selectCompleted;
        public int Select { get; private set; }
        List<int> charaIndexList;
        public CharaSelectWindow(MenuComponent parent, MenuDescription desc, List<int> charaIndexList, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            this.charaIndexList = charaIndexList;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels() =>
            charaIndexList.Select(e => new CharaIndexPanel(e.ToString(), 1, this)).ToArray();

        public override void Update()
        {
            base.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
        }

        public override void OnInputSubmit()
        {
            Select = charaIndexList[Index];
            selectCompleted.Val = true;
        }
        //public override void OnInputCancel() { Quit(); ReturnParent(); }
        public override void Quit()
        {
            if (IsActive)
                base.Quit();
            IsActive = false;
        }
    }
}
