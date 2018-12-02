using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil
{
    class CommandSelectWindow : BasicMenu
    {
        Reference<bool> selectCompleted;
        public CommandEnum Select { get; private set; }
        public ActionName SelectAction { get; private set; }
        public int SelectTarget { get; private set; }
        MagicSelectWindow msw;//magic
        MagicSelectWindow ssw;//skill
        public CommandSelectWindow(MenuComponent parent, MenuDescription desc, Reference<bool> selectCompleted)
            : base(parent, desc)
        {
            msw = new MagicSelectWindow(this, desc, new List<ActionName>()
                {
                    ActionName.NormalAttack,
                    ActionName.ExampleMagic,
                }, selectCompleted);
            ssw = new MagicSelectWindow(this, desc, new List<ActionName>()
                {
                    ActionName.ExampleDebuff
                }, selectCompleted);
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
}
