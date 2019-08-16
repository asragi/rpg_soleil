﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil.Battle
{
    /// <summary>
    /// ターンでの行動の基底
    /// AttackとBuffの基底
    /// </summary>
    abstract class Action
    {
        public Range.AttackRange ARange
        {
            get; protected set;
        }
        protected int MP;
        public Action(Range.AttackRange aRange, int mp = 0)
        {
            ARange = aRange;
            MP = mp;
        }

        protected static readonly BattleField BF = BattleField.GetInstance();
        protected bool HasSufficientMP = true;

        public virtual Action Generate(Range.AttackRange aRange)
        {
            var tmp = (Action)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        public List<Occurence> Act()
        {
            var cEffects = CollectConditionedEffects(new List<ConditionedEffect>());
            cEffects = CheckMP(cEffects);
            return AggregateConditionEffects(cEffects);
        }

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

        public abstract List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects);



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
