using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil.Battle
{
    /// <summary>
    /// SkillIDの選択Window
    /// </summary>
    class SkillSelectWindow : SkillMenu
    {
        protected override Vector WindowPos => new Vector(450, 100);
        Reference<bool> selectCompleted;
        public SelectItems Select;
        MenuComponent parent;
        MenuDescription desc;

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
        public SkillSelectWindow(MenuComponent parent, MenuDescription desc, Person person, Reference<bool> selectCompleted, int charaIndex)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            this.parent = parent;
            this.desc = desc;
            this.charaIndex = charaIndex;

            SetPerson(person);
            IsQuit = true;
        }

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
            var nowPanel = (SkillMenuPanel)Panels[Index];
            //magicが一つもないとき？
            Select.AName = nowPanel.ID;
            Select.ARange = ActionInfo.GetAction(Select.AName).ARange.Clone();
            Select.ARange.SourceIndex = charaIndex;
            switch (Select.ARange)
            {
                case Range.OneEnemy oe:
                    csw = new CharaSelectWindow(this, desc, bf.OppositeIndexes(charaIndex), selectCompleted);
                    csw.Call();
                    Inactivate();
                    break;
                case Range.Ally a:
                    csw = new CharaSelectWindow(this, desc, bf.SameSideIndexes(charaIndex), selectCompleted);
                    csw.Call();
                    Inactivate();
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

        public void Activate()
        {
            base.Call();
            IsActive = true;
        }
        public void Inactivate()
        {
            base.Quit();
            IsActive = false;
        }
    }
}
