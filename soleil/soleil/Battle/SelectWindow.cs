using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                sb.DrawText(Position + new Vector(80, i*30), Resources.GetFont(FontID.Test), choiceList[i], Color.White, DepthID.Message);

            sb.DrawBox(Position + new Vector(0, (int)SelectCommand * 30), new Vector(5, 5), Color.White, DepthID.Message);
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
}
