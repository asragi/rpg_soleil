﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// 条件付きで戦闘中などに効果を発動するもの
    /// </summary>
    using Condition = Func<Action, bool>;
    using AffectFunc = Func<Action, List<Occurence>, List<Occurence>>;
    class ConditionedEffect : IComparable
    {
        protected static readonly BattleField BF = BattleField.GetInstance();
        static int counter = 0;
        int count;
        int priority;
        public int CompareTo(ConditionedEffect ce)
        {
            var p = priority.CompareTo(ce.priority) * -1;
            if (p != 0) return p;
            return count.CompareTo(ce.count);
        }

        public int CompareTo(object obj)
        {
            if (obj is ConditionedEffect ce) return CompareTo(ce);
            throw new NotImplementedException();
        }

        public Condition Cond;
        public AffectFunc Affect;
        protected Func<bool> disable;
        public ConditionedEffect(Condition cond, AffectFunc affect, int priority_)
        {
            count = counter;
            counter++;

            Cond = cond;
            Affect = affect;
            priority = priority_;
            disable = () => false;
        }

        public ConditionedEffect(Condition cond, AffectFunc affect, int priority_, Func<bool> isAvailable)
        {
            count = counter;
            counter++;

            Cond = cond;
            Affect = affect;
            priority = priority_;
            disable = isAvailable;
        }

        public List<Occurence> Act(Action action, List<Occurence> ocrs)
            => Cond(action) ? Affect(action, ocrs) : ocrs;

        public bool Expired() => disable();
    }

    /// <summary>
    /// 条件付きで戦闘中などに効果を発動するもので、
    /// 一定時間で効果時間が切れるもの
    /// </summary>
    class ConditionedEffectWithExpireTime : ConditionedEffect
    {
        int expireTime;
        public ConditionedEffectWithExpireTime(Condition cond, AffectFunc affect, int priority_, int charaIndex, int expireTime_)
            : base(cond, affect, priority_)
        {
            expireTime = expireTime_;
            disable = () =>
            {
                return BF.GetCharacter(charaIndex).Status.WP > expireTime;
            };
        }
    }


    /// <summary>
    /// 一度だけ発動する
    /// </summary>
    class ConditionedEffectOnce : ConditionedEffect
    {
        bool flag = false;
        public ConditionedEffectOnce(Condition cond, AffectFunc affect, int priority_)
            : base(cond, affect, priority_)
        {
            Cond = act => { flag = cond(act); return flag; };
            disable = () => flag;
        }
        public ConditionedEffectOnce(Condition cond, AffectFunc affect, int priority_, Func<bool> isAvailable)
            : base(cond, affect, priority_)
        {
            Cond = act => { flag = cond(act); return flag; };
            disable = () => { return flag || isAvailable(); };
        }
    }
}
