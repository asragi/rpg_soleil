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
        BattleField bf;
        Action genAkt;

        CommandSelectWindow2 commandSelect;
        Menu.MenuDescription desc;
        Reference<bool> doSelect;
        public DefaultPlayableCharacterCommandSelect(BattleField bf, int charaIndex) : base(bf, charaIndex)
        {
            this.bf = bf;

            doSelect = new Reference<bool>(false);
            desc = new Menu.MenuDescription(new Vector(300, 50));
            commandSelect = new CommandSelectWindow2(new Menu.MenuDescription(new Vector()), desc, doSelect);
            bf.AddBasicMenu(commandSelect);
            bf.AddBasicMenu(desc);
        }

        bool first = true;
        public override bool GetAction(Turn turn)
        {
            if (first)
            {
                commandSelect.Call();
                first = false;
            }
            if (!doSelect.Val)
                return false;

            doSelect.Val = false;
            first = true;
            commandSelect.Quit();
            Action action = null;
            switch (commandSelect.Select)
            {
                case CommandEnum.Magic:
                case CommandEnum.Skill:
                    {
                        genAkt = AttackInfo.GetAction(commandSelect.SelectAction);
                        switch (genAkt)
                        {
                            case Attack atk:
                                action = atk.GenerateAttack(new Range.OneEnemy(CharaIndex, commandSelect.SelectTarget));
                                break;
                            case Buff buf:
                                action = buf.GenerateAttack(new Range.OneEnemy(CharaIndex, commandSelect.SelectTarget));
                                break;
                            default:
                                throw new Exception("not implemented");
                        }
                        return true;
                    }
                case CommandEnum.Guard:
                    bf.AddCEffect(new ConditionedEffectWithExpireTime(
                        (bf, act) => {
                            if (act is Attack atk)
                            {
                                return atk.ARange.ContainRange(CharaIndex, bf);
                            }
                            return false;
                        },
                        (bf, act, ocrs) => { var atk = (Attack)act; atk.DamageF *= 0.75f; ocrs.Add(new Occurence("ガードによりダメージが軽減した")); return ocrs; },
                        100000, CharaIndex, turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.TurnWP
                        ));
                    return true;
                case CommandEnum.Escape:
                    action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                    EnqueueTurn(action, turn);
                    return true;
            }
            
            return false;
        }
    }
    /*
    class DefaultPlayableCharacterCommandSelect : CommandSelect
    {
        Stack<BattleUI> windows;
        BattleField bf;
        Action genAkt;
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
                //var sw = new CommandSelectWindow(new Vector(600, 200));
                var sw = new CommandSelectWindow(new Vector(600, 200), SelectPhase.Initial);
                windows.Push(sw);
                bf.AddUI(sw);
            }

            var top = (VerticalSelectWindow)windows.Peek();
            var cmd = top.Select();
            if (!cmd.HasValue) return false;
            Action action = null;
            switch (top.SPhase)
            {
                case SelectPhase.Initial:
                    //debug なにを選んでも攻撃
                    switch (cmd.Value)
                    {
                        case 0://Command.Magic:
                            var swm = new VerticalSelectWindow(new Vector(700, 300), new List<string> { "NormalAttack", "NormalAttack" }, SelectPhase.Magic);
                            windows.Push(swm);
                            bf.AddUI(swm);
                            break;
                        case 1://Command.Skill:
                            //var sws = new VerticalSelectWindow(new Vector(700, 300), new List<string> { "ExampleDebuff", "ExampleDebuff" }, SelectPhase.Skill);
                            var sws = new MagicMenuWindow(new Vector(700, 300), new List<string> { "ExampleDebuff", "ExampleDebuff" }, SelectPhase.Skill);
                            windows.Push(sws);
                            bf.AddUI(sws);
                            break;
                        case 2://Command.Guard:
                            //act = ((Buff)AttackInfo.GetAction(ActionName.Guard)).GenerateAttack(new Range.Me(CharaIndex));
                            //EnqueueTurn(act, turn);
                            //act = ((Buff)AttackInfo.GetAction(ActionName.EndGuard)).GenerateAttack(new Range.Me(CharaIndex));
                            //BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.TurnWP + 100, turn.CStatus, turn.CharaIndex, act));
                            
                            bf.AddCEffect(new ConditionedEffectWithExpireTime(
                                (bf, act) => { if(act is Attack atk) {
                                        return atk.ARange.ContainRange(CharaIndex, bf); } return false; },
                                (bf, act, ocrs) => { var atk = (Attack)act; atk.DamageF *= 0.75f; ocrs.Add(new Occurence("ガードによりダメージが軽減した")); return ocrs; },
                                100000, CharaIndex, turn.WaitPoint + bf.GetCharacter(CharaIndex).Status.TurnWP
                                ));
                            retExec();
                            return true;
                        case 3://Command.Escape:
                            action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                            EnqueueTurn(action, turn);
                            retExec();
                            return true;
                        default:
                            break;
                    }

                    break;
                case SelectPhase.Magic:
                    if (true)
                    {
                        genAkt = AttackInfo.GetAction(ActionName.NormalAttack);
                        var sws = new VerticalSelectWindow(new Vector(750, 350), bf.OppositeIndexes(CharaIndex).Select(p=>p.ToString()).ToList(), SelectPhase.Character);
                        windows.Push(sws);
                        bf.AddUI(sws);
                        break;
                    }
                    else
                    {
                        action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                        EnqueueTurn(action, turn);
                        retExec();
                        return true;
                    }
                case SelectPhase.Skill:
                    //全体攻撃など選択する必要のないとき
                    //TODO: 何を選択するのかをいい感じに記述する
                    if (true)
                    {
                        genAkt = AttackInfo.GetAction(ActionName.ExampleDebuff);
                        var sws = new VerticalSelectWindow(new Vector(750, 350), bf.OppositeIndexes(CharaIndex).Select(p => p.ToString()).ToList(), SelectPhase.Character);
                        windows.Push(sws);
                        bf.AddUI(sws);
                        break;
                    }
                    else
                    {
                        action = ((Buff)AttackInfo.GetAction(ActionName.ExampleDebuff)).GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex).First()));
                        EnqueueTurn(action, turn);
                        retExec();
                        return true;
                    }
                case SelectPhase.Character:
                    switch(genAkt)
                    {
                        case Attack atk:
                            action = atk.GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex)[cmd.Value]));
                            break;
                        case Buff buf:
                            action = buf.GenerateAttack(new Range.OneEnemy(CharaIndex, bf.OppositeIndexes(CharaIndex)[cmd.Value]));
                            break;
                        default:
                            throw new Exception("not implemented");
                    }
                    EnqueueTurn(action, turn);
                    retExec();
                    return true;
                default:
                    throw new Exception("??????");
            }
            
            
            return false;
        }
    }*/
}
