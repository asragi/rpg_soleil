﻿using System;
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
            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + 100, turn.CStatus, turn.CharaIndex, action));
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
            EnqueueTurn(((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, target)), turn);
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

                    Action action = null;
                    //debug なにを選んでも攻撃
                    switch (cmd.Value)
                    {
                        case Command.Magic:
                            action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(action, turn);
                            retExec();
                            return true;
                        case Command.Skill:
                            action = ((Buff)AttackInfo.GetAction(ActionName.ExampleDebuff)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(action, turn);
                            retExec();
                            return true;
                        case Command.Guard:
                            /*
                            act = ((Buff)AttackInfo.GetAction(ActionName.Guard)).GenerateAttack(new Range.Me(CharaIndex));
                            EnqueueTurn(act, turn);
                            act = ((Buff)AttackInfo.GetAction(ActionName.EndGuard)).GenerateAttack(new Range.Me(CharaIndex));
                            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.TurnWP + 100, turn.CStatus, turn.CharaIndex, act));
                            */
                            bf.AddCEffect(new ConditionedEffectWithExpireTime(
                                (bf, act) => { if(act is Attack atk) {
                                        return atk.ARange.ContainRange(CharaIndex, bf); } return false; },
                                (bf, act, ocrs) => { var atk = (Attack)act; atk.DamageF *= 0.75f; ocrs.Add(new Occurence("ガードによりダメージが軽減した")); return ocrs; },
                                100000, CharaIndex, turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.TurnWP
                                ));
                            retExec();
                            return true;
                        case Command.Escape:
                            action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(action, turn);
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
