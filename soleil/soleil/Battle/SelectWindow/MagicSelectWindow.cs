using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil.Battle
{
    /// <summary>
    /// アイテムメニューのそれぞれの選択パネルのクラス
    /// </summary>
    class MagicSelectPanel : TextSelectablePanel
    {
        public readonly Vector CostPosDiff = new Vector(300, 0);
        public override string Desctiption => ItemName;

        public MagicSelectPanel(String itemName, int num, MagicSelectWindow parent)
            : base(itemName, parent)
        {
            // itemNum
            Val = num;
            LocalPos = Vector.Zero;
        }
    }

    /// <summary>
    /// SkillIDの選択Window
    /// </summary>
    class MagicSelectWindow : BasicMenu
    {
        protected override Vector WindowPos => new Vector(450, 100);
        Reference<bool> selectCompleted;
        public SelectItems Select;
        MenuComponent parent;
        MenuDescription desc;

        /// <summary>
        /// Window上の選択肢
        /// </summary>
        List<Skill.SkillID> magicList;
        int charaIndex;
        static readonly BattleField bf = BattleField.GetInstance();

        /// <summary>
        /// 攻撃対象の選択window
        /// Actionを選択してから実体化する
        /// </summary>
        CharaSelectWindow csw;

        /// <summary>
        /// このWindowが開かれているかのフラグ
        /// IsActiveは開かれていても選択されていない場合はfalseとなる為必要
        /// </summary>
        bool IsQuit;
        public MagicSelectWindow(MenuComponent parent, MenuDescription desc, List<Skill.SkillID> list, Reference<bool> selectCompleted, int charaIndex)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            magicList = list;
            this.parent = parent;
            this.desc = desc;
            this.charaIndex = charaIndex;
            IsQuit = true;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels() =>
            magicList.Select(e => new MagicSelectPanel(ActionInfo.GetActionName(e), 1, this)).ToArray();

        public override void Update()
        {
            base.Update();
            csw?.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
            else
                csw?.UpdateInput();



            switch (Select.ARange)
            {
                case Range.OneEnemy oe:
                    if (csw != null)
                        oe.TargetIndex = csw.SelectIndex;
                    break;
                case Range.Ally a:
                    if (csw != null)
                        a.TargetIndex = csw.SelectIndex;
                    break;
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            csw?.Draw(d);
        }

        /// <summary>
        /// 選択したActionのAttackRangeによってCharaSelectWindowを呼び出すか決める
        /// </summary>
        public override void OnInputSubmit()
        {
            if (magicList.Count == 0) return;
            Select.AName = magicList[Index];
            Select.ARange = ActionInfo.GetAction(Select.AName).ARange.Clone();
            Select.ARange.SourceIndex = charaIndex;
            switch (Select.ARange)
            {
                case Range.OneEnemy oe:
                    csw = new CharaSelectWindow(this, desc, bf.OppositeIndexes(charaIndex), selectCompleted);
                    csw.Call();
                    break;
                case Range.Ally a:
                    csw = new CharaSelectWindow(this, desc, bf.SameSideIndexes(charaIndex), selectCompleted);
                    csw.Call();
                    break;
                case Range.AllAlly aRange:
                    aRange.TargetSide = bf.GetSide(charaIndex);
                    selectCompleted.Val = true;
                    break;
                case Range.AllEnemy aRange:
                    aRange.TargetSide = bf.OppositeSide(charaIndex);
                    selectCompleted.Val = true;
                    break;
                default:
                    selectCompleted.Val = true;
                    break;
            }
            IsActive = false;
        }
        public override void OnInputCancel() { Quit(); ReturnParent(); }

        public override void Call()
        {
            base.Call();
            IsQuit = false;
        }

        public override void Quit()
        {
            csw?.Quit();
            if (!IsQuit)
                base.Quit();
            IsQuit = true;
            IsActive = false;

        }
    }
}
