using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil
{
    class MagicSelectWindow : MagicMenu
    {
        Reference<bool> selectCompleted;
        public ActionName Select { get; private set; }
        List<ActionName> magicList;
        MenuComponent parent;
        MenuDescription desc;

        /*
         攻撃対象の選択window
         Actionを選択してから実体化する
             */
        CharaSelectWindow csw;
        public MagicSelectWindow(MenuComponent parent, MenuDescription desc, List<ActionName> list, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            magicList = list;
            this.parent = parent;
            this.desc = desc;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels() =>
            magicList.Select(e => new MagicMenuPanel(AttackInfo.GetActionName(e), 1, this)).ToArray();

        public override void Update()
        {
            base.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
            csw?.Update();
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            csw?.Draw(d);
        }

        public override void OnInputSubmit()
        {
            Select = magicList[Index];
            switch (AttackInfo.GetAction(Select).ARange)
            {
                case Range.OneEnemy oe:
                    csw = new CharaSelectWindow(parent, desc, new List<int> { }, selectCompleted);
                    csw.Call();
                    break;
            }
            //Todo: Range選択
            selectCompleted.Val = true;
        }
        //public override void OnInputCancel() { Quit(); ReturnParent(); }
        public override void Quit()
        {
            if (IsActive)
                base.Quit();
            csw?.Quit();
            IsActive = false;
        }
    }
}
