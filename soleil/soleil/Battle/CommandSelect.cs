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
        public abstract bool GetAction(Turn turn);
        protected void EnqueueTurn(Action action, Turn turn)
        {
            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + 100, turn.SPD, turn.CharaIndex, action));
        }
    }

    class DefaultCharacterCommandSelect : CommandSelect
    {
        public DefaultCharacterCommandSelect(BattleField bf, int charaIndex) : base(bf, charaIndex)
        {
        }

        public override bool GetAction(Turn turn)
        {
            var indexes = BF.OppositeIndexes(CharaIndex);
            int target = indexes[Global.Random(indexes.Count)];
            EnqueueTurn(((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new OneEnemy(CharaIndex, target)), turn);
            return true;
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

        
        public override bool GetAction(Turn turn)
        {
            System.Action retExec = () =>
            {
                windows.ForEach2(e => bf.RemoveUI(e));
                windows.Clear();
            };

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
                    if (!cmd.HasValue) return false;

                    Action act = null;
                    //debug なにを選んでも攻撃
                    switch (cmd.Value)
                    {
                        case Command.Magic:
                            act = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(act, turn);
                            retExec();
                            return true;
                        case Command.Skill:
                            act = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(act, turn);
                            retExec();
                            return true;
                        case Command.Guard:
                            act = ((Buff)AttackInfo.GetAction(ActionName.Guard)).GenerateAttack(new Me(CharaIndex));
                            EnqueueTurn(act, turn);
                            act = ((Buff)AttackInfo.GetAction(ActionName.EndGuard)).GenerateAttack(new Me(CharaIndex));
                            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.WP + 100, turn.SPD, turn.CharaIndex, act));
                            retExec();
                            return true;
                        case Command.Escape:
                            act = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(act, turn);
                            retExec();
                            return true;
                        default:
                            break;
                    }

                    break;
                default:
                    throw new Exception("??????");
            }
            
            
            return false;
        }
    }
}
