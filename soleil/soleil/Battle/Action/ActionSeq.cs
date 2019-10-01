using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil.Battle
{
    /// <summary>
    /// Actionの列
    /// actionsのmpは0に指定すること
    /// TODO:Actions の ARangeが違う場合の処理
    /// </summary>
    class ActionSeq : Action
    {
        //Seqの攻撃対象どうしようね
        List<Action> Actions;
        public ActionSeq(List<Action> actions, Range.AttackRange aRange, int mp = 0)
            : base(aRange, mp) => Actions = actions;

        public override Action Generate(Range.AttackRange aRange)
        {
            var tmp = (ActionSeq)MemberwiseClone();
            tmp.ARange = aRange;
            tmp.Actions = tmp.Actions.Select<Action, Action>(act => act.Generate(aRange)).ToList();
            return tmp;
        }

        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            var cEfs = Actions.Aggregate(cEffects, (cefs, act) => act.CollectConditionedEffects(cefs));
            return cEfs;
        }
    }



}
