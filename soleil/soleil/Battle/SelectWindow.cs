using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil
{
    enum CommandEnum
    {
        Magic,
        Skill,
        Guard,
        Escape,
    }
    class CommandPanel : SelectablePanel
    {
        public override string Desctiption => "a";
        readonly CommandEnum command;
        UIImage selected, unselected;
        public CommandPanel(CommandEnum command, BasicMenu parent):base(parent)
        {
            this.command = command;
            TextureID selectedID, unselectedID;
            switch(command)
            {
                case CommandEnum.Magic:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedMagic, TextureID.BattleCommandUnselectedMagic);
                    break;
                case CommandEnum.Skill:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedSkill, TextureID.BattleCommandUnselectedSkill);
                    break;
                case CommandEnum.Guard:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedGuard, TextureID.BattleCommandUnselectedGuard);
                    break;
                case CommandEnum.Escape:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedEscape, TextureID.BattleCommandUnselectedEscape);
                    break;
                default:
                    (selectedID, unselectedID) = (0, 0);
                    break;
            }
            selected = new UIImage(selectedID, LocalPos + parent.Pos, ItemNumPosDiff, DepthID.Status);
            unselected = new UIImage(unselectedID, LocalPos + parent.Pos, ItemNumPosDiff, DepthID.Status);
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            if (isFadeIn)
            {
                if (IsSelected)
                    selected.Fade(duration, _easeFunc, isFadeIn);
                else
                    unselected.Fade(duration, _easeFunc, isFadeIn);
            }
            else
                selected.Fade(duration, _easeFunc, isFadeIn);
                unselected.Fade(duration, _easeFunc, isFadeIn);

        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            selected.MoveTo(target, duration, _easeFunc);
            unselected.MoveTo(target, duration, _easeFunc);
        }

        protected override void OnSelected()
        {
            selected.Fade(20, MenuSystem.EaseFunc, true);
            unselected.Fade(20, MenuSystem.EaseFunc, false);
        }

        /// <summary>
        /// 選択が解除された瞬間に行われる処理．
        /// </summary>
        protected override void OnUnselected()
        {
            selected.Fade(5, MenuSystem.EaseFunc, false);
            unselected.Fade(5, MenuSystem.EaseFunc, true);
        }

        public override void Update()
        {
            selected.Update();
            selected.Pos = LocalPos + BasicMenu.Pos;
            unselected.Update();
            unselected.Pos = LocalPos + BasicMenu.Pos;
        }

        public override void Draw(Drawing d)
        {
            selected.Draw(d);
            unselected.Draw(d);
        }
    }

    class CommandSelectWindow2 : BasicMenu
    {
        Reference<bool> selectCompleted;
        public CommandEnum Select { get; private set; }
        public ActionName SelectAction { get; private set; }
        public int SelectTarget { get; private set; }
        MagicSelectWindow msw;
        SkillSelectWindow ssw;
        public CommandSelectWindow2(MenuComponent parent, MenuDescription desc, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            msw = new MagicSelectWindow(this, desc, selectCompleted);
            ssw = new SkillSelectWindow(this, desc, selectCompleted);
            this.selectCompleted = selectCompleted;
            SelectTarget = 2;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            return new CommandPanel[]
            {
                new CommandPanel(CommandEnum.Magic, this),
                new CommandPanel(CommandEnum.Skill, this),
                new CommandPanel(CommandEnum.Guard, this),
                new CommandPanel(CommandEnum.Escape, this),
            };
        }

        public override void Update()
        {
            base.Update();
            msw.Update();
            ssw.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));

            if (Select == CommandEnum.Magic)
                SelectAction = msw.Select;
            else if (Select == CommandEnum.Skill)
                SelectAction = ssw.Select;
        }
        public override void Draw(Drawing d)
        {
            //base.Draw(d);
            
            foreach (var item in Panels)
            {
                item?.Draw(d);
            }
            msw.Draw(d);
            ssw.Draw(d);
        }


        public override void OnInputSubmit()
        {
            Select = (CommandEnum)Index;
            switch (Select)
            {
                case CommandEnum.Magic:
                    msw.Call();
                    break;
                case CommandEnum.Skill:
                    ssw.Call();
                    break;
                default:
                    selectCompleted.Val = true;
                    break;
            }
            IsActive = false;
        }
        public override void OnInputCancel() { }
        public override void Quit()
        {
            IsActive = false;
            base.Quit();
            msw.Quit();
            ssw.Quit();
        }
    }

    class MagicSelectWindow : MagicMenu
    {
        Reference<bool> selectCompleted;
        public ActionName Select { get; private set; }
        public MagicSelectWindow(MenuComponent parent, MenuDescription desc, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            return new TextSelectablePanel[]
            {
                new MagicMenuPanel("NormalAttack", 8, this),
                new MagicMenuPanel("ExampleMagic", 40, this),
            };
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
        }

        public override void OnInputSubmit()
        {
            Select = ActionName.NormalAttack;
            //Todo: Range選択
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

    class SkillSelectWindow : MagicMenu
    {
        Reference<bool> selectCompleted;
        public ActionName Select { get; private set; }
        public SkillSelectWindow(MenuComponent parent, MenuDescription desc, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            this.selectCompleted = selectCompleted;
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            return new TextSelectablePanel[]
            {
                new MagicMenuPanel("ExampleDefuff", 8, this),
            };
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
                Input(KeyInput.GetStickFlickDirection(1));
        }

        public override void OnInputSubmit()
        {
            Select = ActionName.ExampleDebuff;
            //Todo: Range選択
            selectCompleted.Val = true;
        }
        //public override void OnInputCancel() { Quit(); ReturnParent(); }
        public override void Quit()
        {
            if(IsActive)
                base.Quit();
            IsActive = false;
        }
    }
}
