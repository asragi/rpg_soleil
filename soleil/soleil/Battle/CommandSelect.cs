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
        Stack<BattleUI> windows;
        BattleField bf;
        public DefaultPlayableCharacterCommandSelect(BattleField bf, int charaIndex) : base(bf, charaIndex)
        {
            this.bf = bf;
            windows = new Stack<BattleUI>();
        }

        
        public override Action GetAction()
        {
            Action act = null;
            if(windows.Count==0)
            {
                var sw = new CommandSelectWindow(new Vector(600, 200));
                windows.Push(sw);
                bf.AddUI(sw);
            }

            var top = windows.Peek();
            switch (top)
            {
                case CommandSelectWindow csw:
                    var cmd = csw.Select();
                    if (!cmd.HasValue) return null;

                    //debug なにを選んでも攻撃
                    switch (cmd.Value)
                    {
                        case Command.Magic:
                            act = ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(CharaIndex, bf.OppositeIndexes(CharaIndex).First());
                            break;
                        case Command.Skill:
                            act = ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(CharaIndex, bf.OppositeIndexes(CharaIndex).First());
                            break;
                        case Command.Guard:
                            act = ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(CharaIndex, bf.OppositeIndexes(CharaIndex).First());
                            break;
                        case Command.Escape:
                            act = ((AttackForOne)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(CharaIndex, bf.OppositeIndexes(CharaIndex).First());
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    throw new Exception("??????");
            }
            

            if(act!=null)
            {
                windows.ForEach2(e => bf.RemoveUI(e));
                windows.Clear();
                return act;
            }
            return null;
        }
    }
}
