using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    abstract class CommandSelect
    {
        public int CharaIndex = -1;
        protected static readonly BattleField BF = BattleField.GetInstance();
        public CommandSelect(int charaIndex) => CharaIndex = charaIndex;

        /// <returns>選択が完了したかどうか</returns>
        public abstract bool GetAction(Turn turn);
        protected void EnqueueTurn(Action action, Turn turn)
        {
            BF.EnqueueTurn(new ActionTurn(turn.WaitPoint + 100, turn.CStatus, turn.CharaIndex, action));
        }
    }

    /// <summary>
    /// 暫定的な敵のコマンドセレクトアルゴリズム
    /// </summary>
    class DefaultCharacterCommandSelect : CommandSelect
    {
        public DefaultCharacterCommandSelect(int charaIndex) : base(charaIndex)
        {
        }

        public override bool GetAction(Turn turn)
        {
            var indexes = BF.OppositeIndexes(CharaIndex);
            int target = indexes[Global.Random(indexes.Count)];
            EnqueueTurn((ActionInfo.GetAction(Skill.SkillID.NormalAttack)).Generate(new Range.OneEnemy(CharaIndex, target)), turn);
            return true;
        }
    }


    /// <summary>
    /// 自機のコマンドセレクトアルゴリズム
    /// </summary>
    class DefaultPlayableCharacterCommandSelect : CommandSelect
    {
        Action genAkt;

        CommandSelectWindow commandSelect;
        Menu.MenuDescription desc;

        /// <summary>
        /// 行動選択が終わったかどうかをcommandSelectから取得するためのもの
        /// </summary>
        Reference<bool> doSelect;
        public DefaultPlayableCharacterCommandSelect(int charaIndex, CharacterStatus status, Person person) : base(charaIndex)
        {
            doSelect = new Reference<bool>(false);
            desc = new Menu.MenuDescription(new Vector(300, 50));
            commandSelect = new CommandSelectWindow(new Menu.MenuDescription(new Vector()), desc, doSelect, charaIndex, person);
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
                        genAkt = ActionInfo.GetAction(commandSelect.Select.AName);
                        action = genAkt.Generate(commandSelect.Select.ARange);
                        /*
                        switch (genAkt)
                        {
                            case Attack atk:
                                action = atk.GenerateAttack(commandSelect.Select.ARange);
                                break;
                            case Buff buf:
                                action = buf.GenerateBuff(commandSelect.Select.ARange);
                                break;
                            case Heal heal:
                                action = heal.GenerateHeal(commandSelect.Select.ARange);
                                break;
                            case ActionSeq seq:
                                action = seq.GenerateActionSeq(commandSelect.Select.ARange);
                                break;
                            default:
                                throw new Exception("not implemented");
                        }
                        */
                        EnqueueTurn(action, turn);
                        return true;
                    }
                case CommandEnum.Guard:
                    BF.AddCEffect(new ConditionedEffectWithExpireTime(
                        (act) =>
                        {
                            if (act is Attack atk)
                            {
                                return atk.ARange.ContainRange(CharaIndex, BattleField.GetInstance());
                            }
                            return false;
                        },
                        (act, ocrs) => { var atk = (Attack)act; atk.DamageF *= 0.75f; ocrs.Add(new Occurence("ガードによりダメージが軽減した")); return ocrs; },
                        90000, CharaIndex, turn.WaitPoint + BF.GetCharacter(CharaIndex).Status.TurnWP
                        ));
                    return true;
                case CommandEnum.Escape:
                    action = (ActionInfo.GetAction(Skill.SkillID.Escape)).Generate(new Range.AllEnemy(CharaIndex, BF.OppositeSide(CharaIndex)));
                    EnqueueTurn(action, turn);
                    return true;
            }

            return false;
        }
    }
}
