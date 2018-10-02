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
}
