using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    abstract class CommandSelect
    {
        public int CharaIndex = -1;
        protected BattleField BF;
        public CommandSelect(BattleField bf, int charaIndex) => (BF, CharaIndex) = (bf, charaIndex);
        public abstract Action GetAction();
    }

    class DefaultCharacterCommandSelect : CommandSelect
    {
        public DefaultCharacterCommandSelect(BattleField bf, int charaIndex) : base(bf, charaIndex)
        {
        }

        public override Action GetAction()
        {
            var indexes = BF.OppositeIndexes(CharaIndex);
            int target = indexes[Global.Random(indexes.Count)];
            return ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(CharaIndex, target);
        }
    }

    class DefaultPlayableCharacterCommandSelect : CommandSelect
    {
        public DefaultPlayableCharacterCommandSelect(BattleField bf, int charaIndex) : base(bf, charaIndex)
        {
        }


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
