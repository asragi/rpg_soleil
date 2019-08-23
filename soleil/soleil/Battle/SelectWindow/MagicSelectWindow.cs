﻿using System;
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

    class MagicSelectWindow : BasicMenu
    {
        protected override Vector WindowPos => new Vector(450, 100);
        Reference<bool> selectCompleted;
        public SelectItems Select;
        List<Skill.SkillID> magicList;
        MenuComponent parent;
        MenuDescription desc;
        int charaIndex;
        static readonly BattleField bf = BattleField.GetInstance();
        /*
         攻撃対象の選択window
         Actionを選択してから実体化する
             */
        CharaSelectWindow csw;
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

        public override void OnInputSubmit()
        {
            Select.AName = magicList[Index];
            Select.ARange = ActionInfo.GetAction(Select.AName).ARange.Clone();
            Select.ARange.SourceIndex = charaIndex;
            switch (Select.ARange)
            {
                case Range.OneEnemy oe:
                    oe.SourceIndex = charaIndex;
                    csw = new CharaSelectWindow(this, desc, bf.OppositeIndexes(charaIndex), selectCompleted);
                    csw.Call();
                    break;
                case Range.Ally a:
                    a.SourceIndex = charaIndex;
                    csw = new CharaSelectWindow(this, desc, bf.SameSideIndexes(charaIndex), selectCompleted);
                    csw.Call();
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