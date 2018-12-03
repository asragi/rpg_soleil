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
        protected static readonly BattleField BF = BattleField.GetInstance();
        public CommandSelect(int charaIndex) => CharaIndex = charaIndex;
        public abstract bool GetAction(Turn turn);
        protected void EnqueueTurn(Action action, Turn turn)
        {
            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + 100, turn.CStatus, turn.CharaIndex, action));
        }
    }

    class DefaultCharacterCommandSelect : CommandSelect
    {
        public DefaultCharacterCommandSelect(int charaIndex) : base(charaIndex)
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
        Action genAkt;

        CommandSelectWindow commandSelect;
        Menu.MenuDescription desc;
        Reference<bool> doSelect;
        public DefaultPlayableCharacterCommandSelect(int charaIndex) : base(charaIndex)
        {
            doSelect = new Reference<bool>(false);
            desc = new Menu.MenuDescription(new Vector(300, 50));
            commandSelect = new CommandSelectWindow(new Menu.MenuDescription(new Vector()), desc, doSelect, charaIndex);
            BF.AddBasicMenu(commandSelect);
            BF.AddBasicMenu(desc);
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
            switch (commandSelect.Select.Command)
            {
                case CommandEnum.Magic:
                case CommandEnum.Skill:
                    {
                        genAkt = AttackInfo.GetAction(commandSelect.Select.AName);
                        switch (genAkt)
                        {
                            case Attack atk:
                                action = atk.GenerateAttack(commandSelect.Select.ARange);
                                break;
                            case Buff buf:
                                action = buf.GenerateAttack(commandSelect.Select.ARange);
                                break;
                            default:
                                throw new Exception("not implemented");
                        }
                        EnqueueTurn(action, turn);
                        return true;
                    }
                case CommandEnum.Guard:
                    BF.AddCEffect(new ConditionedEffectWithExpireTime(
                        (bf, act) =>
                        {
                            if (act is Attack atk)
                            {
                                return atk.ARange.ContainRange(CharaIndex, bf);
                            }
                            return false;
                        },
                        (bf, act, ocrs) => { var atk = (Attack)act; atk.DamageF *= 0.75f; ocrs.Add(new Occurence("ガードによりダメージが軽減した")); return ocrs; },
                        100000, CharaIndex, turn.WaitPoint + BF.GetCharacter(CharaIndex).Status.TurnWP
                        ));
                    return true;
                case CommandEnum.Escape:
                    action = ((Attack)AttackInfo.GetAction(ActionName.NormalAttack)).GenerateAttack(new Range.OneEnemy(CharaIndex, BF.OppositeIndexes(CharaIndex).First()));
                    EnqueueTurn(action, turn);
                    return true;
            }

            return false;
        }
    }
}
