using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil
{
    abstract class BattleUI
    {
        protected Vector Position;
        public BattleUI(Vector pos) => Position = pos;
        public abstract void Draw(Drawing sb);
    }

    /*
    enum Command : int
    {
        Magic,
        Skill,
        Guard,
        Escape,
        Size,
    }

    class CommandSelectWindow : BattleUI
    {
        public CommandSelectWindow(Vector pos):base(pos)
        {

        }
        public override void Draw(Drawing sb)
        {
            sb.DrawText(Position + new Vector(80, 0), Resources.GetFont(FontID.Test), "Magic", Color.White, DepthID.Message);
            sb.DrawText(Position + new Vector(80, 30), Resources.GetFont(FontID.Test), "Skill", Color.White, DepthID.Message);
            sb.DrawText(Position + new Vector(80, 60), Resources.GetFont(FontID.Test), "Guard", Color.White, DepthID.Message);
            sb.DrawText(Position + new Vector(80, 90), Resources.GetFont(FontID.Test), "Escape", Color.White, DepthID.Message);

            sb.DrawBox(Position + new Vector(0, (int)selectCommand * 30), new Vector(5, 5), Color.White, DepthID.Message);
        }

        Command selectCommand=0;
        public Command? Select()
        {
            if (KeyInput.GetKeyPush(Key.Down))
            {
                selectCommand++;
                if (selectCommand == Command.Size)
                {
                    selectCommand = Command.Magic;
                }
            }
            if (KeyInput.GetKeyPush(Key.Up))
            {
                if (selectCommand == 0)
                {
                    selectCommand = Command.Size;
                }
                selectCommand--;
            }

            if (KeyInput.GetKeyPush(Key.A))
            {
                return selectCommand;
            }
            return null;
        }
    }
    */

    abstract class SelectWindow<T> : BattleUI where T : struct
    {
        public SelectPhase SPhase;
        public SelectWindow(Vector pos, SelectPhase selectPhase) : base(pos) => SPhase = selectPhase;
        public abstract T? Select();
        public T GetSelection
        {
            get; protected set;
        }
    }

    enum SelectPhase
    {
        Initial,
        Magic,
        Skill,
        Character,
    }

    //どうせint
    class VerticalSelectWindow : SelectWindow<int>
    {
        List<string> choiceList;
        protected int Size;
        public VerticalSelectWindow(Vector pos, List<string> choiceList, SelectPhase selectPhase) : base(pos, selectPhase)
        {
            this.choiceList = choiceList;
            Size = choiceList.Count;
        }
        public override void Draw(Drawing sb)
        {
            for(int i=0;i<Size;i++)
                sb.DrawText(Position + new Vector(80, i*30), Resources.GetFont(FontID.Test), choiceList[i], Color.White, DepthID.Status);

            sb.DrawBox(Position + new Vector(0, (int)SelectCommand * 30), new Vector(5, 5), Color.White, DepthID.Status);
        }

        protected int SelectCommand=0;
        public override int? Select()
        {
            if (KeyInput.GetKeyPush(Key.Down))
            {
                SelectCommand++;
                if (SelectCommand == Size)
                {
                    SelectCommand = 0;
                }
            }
            if (KeyInput.GetKeyPush(Key.Up))
            {
                if (SelectCommand == 0)
                {
                    SelectCommand = Size;
                }
                SelectCommand--;
            }

            if (KeyInput.GetKeyPush(Key.A))
            {
                GetSelection = SelectCommand;
                return SelectCommand;
            }
            return null;
        }
    }


    class CommandSelectWindow : VerticalSelectWindow
    {
        readonly TextureID[] commandTextureID = new TextureID[]
        {
            TextureID.BattleCommandUnselectedMagic,
            TextureID.BattleCommandUnselectedSkill,
            TextureID.BattleCommandUnselectedGuard,
            TextureID.BattleCommandUnselectedEscape,
            TextureID.BattleCommandSelectedMagic,
            TextureID.BattleCommandSelectedSkill,
            TextureID.BattleCommandSelectedGuard,
            TextureID.BattleCommandSelectedEscape,
        };
        public CommandSelectWindow(Vector pos, SelectPhase selectPhase)
            : base(pos, new List<string>{ "Magic", "Skill", "Guard", "Escape" }, selectPhase)
        {
        }

        public override void Draw(Drawing sb)
        {
            for (int i = 0; i < Size; i++)
                sb.Draw(Position + new Vector(80, i * 30), Resources.GetTexture(commandTextureID[i+(i==SelectCommand?4:0)]), DepthID.Status);
        }
    }


    class MagicMenuWindow : VerticalSelectWindow
    {
        Menu.MagicMenu menu;
        Menu.MenuDescription menuDesc;
        public MagicMenuWindow(Vector pos, List<string> choiceList, SelectPhase selectPhase)
            : base(pos, choiceList, selectPhase)
        {
            menuDesc=new Menu.MenuDescription(new Vector(125, 35));
            menu = new Menu.MagicMenu(new Menu.MenuDescription(new Vector(125, 35)),//Dummy
                menuDesc);
            menu.Call();
        }
        public override void Draw(Drawing sb)
        {
            menuDesc.Draw(sb);
            menu.Draw(sb);
        }

        protected int SelectCommand = 0;
        public override int? Select()
        {
            menu.Update();
            if (KeyInput.GetKeyPush(Key.Down))
            {
                menu.OnInputDown();
                SelectCommand++;
                if (SelectCommand == Size)
                {
                    SelectCommand = 0;
                }
            }
            if (KeyInput.GetKeyPush(Key.Up))
            {
                menu.OnInputUp();
                if (SelectCommand == 0)
                {
                    SelectCommand = Size;
                }
                SelectCommand--;
            }
            menu.Input(Direction.N);

            if (KeyInput.GetKeyPush(Key.A))
            {
                menu.Quit();
                GetSelection = SelectCommand;
                return SelectCommand;
            }
            return null;
        }
    }


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
