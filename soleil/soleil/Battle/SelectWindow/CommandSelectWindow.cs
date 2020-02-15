using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;
using Soleil.Skill;

namespace Soleil.Battle
{
    /// <summary>
    /// CommandEnumを選択できるwindow
    /// </summary>
    class CommandSelectWindow : BasicMenu
    {
        protected override Vector WindowPos => new Vector(750, 200);
        Reference<bool> selectCompleted;
        public SelectItems Select;
        MagicSelectWindow msw;//magic
        SkillSelectWindow ssw;//skill
        public CommandSelectWindow(EasingComponent parent, MenuDescription desc, Reference<bool> selectCompleted, int charaIndex, Person person)
            : base(parent, desc)
        {
            msw = new MagicSelectWindow(this, desc, person, selectCompleted, charaIndex);
            ssw = new SkillSelectWindow(this, desc, person, selectCompleted, charaIndex);
            this.selectCompleted = selectCompleted;
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

            if (Select.Command == CommandEnum.Magic)
            {
                Select = msw.Select;
                Select.Command = CommandEnum.Magic;
            }
            else if (Select.Command == CommandEnum.Skill)
            {
                Select = ssw.Select;
                Select.Command = CommandEnum.Skill;
            }
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
            Select.Command = (CommandEnum)Index;
            switch (Select.Command)
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
