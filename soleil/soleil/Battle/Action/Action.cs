using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil.Battle
{
    /// <summary>
    /// ターンでの行動の基底
    /// Attack,Buff,Heal等の基底
    /// </summary>
    abstract class Action
    {
        /// <summary>
        /// Actionの実行対象
        /// </summary>
        public Range.AttackRange ARange
        {
            get; protected set;
        }


        /// <summary>
        /// 消費MP
        /// </summary>
        protected int MP;


        /// <param name="aRange">実行対象</param>
        /// <param name="mp">消費MP</param>
        public Action(Range.AttackRange aRange, int mp = 0)
        {
            ARange = aRange;
            MP = mp;
        }

        protected static readonly BattleField BF = BattleField.GetInstance();
        protected bool HasSufficientMP = true;

        /// <summary>
        /// ActionInfoのActionはダミーのAttackRangeを持っている
        /// 攻撃対象等を設定したAttackRangeを持つActionを生成する
        /// </summary>
        public virtual Action Generate(Range.AttackRange aRange)
        {
            var tmp = (Action)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        /// <summary>
        /// Action, BattleFieldの持つConditionedEffectを実行する
        /// </summary>
        /// <return> ConditionedEffectを実行した結果OccurenceのList </return>
        public List<Occurence> Act()
        {
            var cEffects = CollectConditionedEffects(new List<ConditionedEffect>());
            cEffects = CheckMP(cEffects);
            return AggregateConditionEffects(cEffects);
        }

        /// <summary>
        /// MP消費判定をするConditionedEffectを追加する関数
        /// </summary>
        List<ConditionedEffect> CheckMP(List<ConditionedEffect> cEffects)
        {
            //MP消費
            cEffects.Add(new ConditionedEffect(
                (act) => true,
                (act, ocrs) =>
                {
                    HasSufficientMP = MP <= BF.GetCharacter(act.ARange.SourceIndex).Status.MP;
                    if (HasSufficientMP)
                    {
                        BF.GetCharacter(act.ARange.SourceIndex).Damage(MP: MP);
                        string mes = BF.GetCharacter(act.ARange.SourceIndex).Name + "のターン！";
                        ocrs.Add(new OccurenceAttackMotion(mes, act.ARange.SourceIndex, MPConsume_: MP));
                    }
                    else
                    {
                        ocrs.Add(new Occurence(BF.GetCharacter(act.ARange.SourceIndex).Name + "はMPが不足している"));
                    }
                    return ocrs;
                }, 100000));


            return cEffects;
        }

        /// <summary>
        /// Actionで実行するConditionedEffectを追加する関数
        /// </summary>
        /// <param name="cEffects"> これまで得たConditionedEffectのList </param>
        /// <returns> 追加した結果得られるConditionedEffectのList </returns>
        public abstract List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects);


        /// <summary>
        /// 引数のConditionedEffectを実行して結果Occurenceを得る
        /// </summary>
        public List<Occurence> AggregateConditionEffects(IEnumerable<ConditionedEffect> additionals, List<Occurence> ocr)
        {
            var ceffects = BF.GetCopiedCEffects();
            ceffects.UnionWith(additionals);
            return ceffects.Aggregate(ocr, (ocrs, ce) => ce.Act(this, ocrs));
        }
        public List<Occurence> AggregateConditionEffects(IEnumerable<ConditionedEffect> additionals)
        {
            return AggregateConditionEffects(additionals, new List<Occurence>());
        }
    }



}
