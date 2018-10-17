using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 条件付きで戦闘中などに効果を発動するもの
    /// </summary>
    using Condition = Func<BattleField, Action, bool>;
    using AffectFunc = Func<BattleField, Action, List<Occurence>, List<Occurence>>;
    class ConditionedEffect : IComparable
    {
        int priority;
        public int CompareTo(ConditionedEffect ce)
        {
            return priority.CompareTo(ce.priority) * -1;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
        
        public Condition Cond;
        public AffectFunc Affect;
        public ConditionedEffect(Condition cond, AffectFunc affect, int priority_)
        {
            Cond = cond;
            Affect = affect;
            priority = priority_;
        }

        public List<Occurence> Act(BattleField bf, Action action, List<Occurence> ocrs)
            => Cond(bf, action) ? Affect(bf, action, ocrs) : ocrs;
    }
}
