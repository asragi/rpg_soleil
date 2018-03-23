using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    abstract class CommandSelect
    {
        public abstract Action GetAction();
    }

    class DefaultCharacterCommandSelect : CommandSelect
    {
        public override Action GetAction()
        {
            return ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(0, 1);
        }
    }

    class DefaultPlayableCharacterCommandSelect : CommandSelect
    {
        bool select = false;
        enum Command
        {
            Attack,
            Magic,
            Item,
            Escape,
            Size,
        }
        Command selectCommand;
        public override Action GetAction()
        {
            if (KeyInput.GetKeyPush(Key.Down))
            {
                selectCommand++;
                if (selectCommand == Command.Size)
                {
                    selectCommand = Command.Attack;
                }
            }
            if (KeyInput.GetKeyPush(Key.Up))
            {
                if (selectCommand == Command.Attack)
                {
                    selectCommand = Command.Size;
                }
                selectCommand--;
            }
            if (KeyInput.GetKeyPush(Key.A))
            {
                return ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(0, 1);
            }

            return null;
        }
    }
}
