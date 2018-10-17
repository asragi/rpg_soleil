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
        
        public Func<BattleField, Action, bool> Cond;
        public Func<BattleField, Action, List<Occurence>> Affect;
    }
}
