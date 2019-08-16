using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil.Battle
{
    /// <summary>
    /// Actionの列
    /// actionsのmpは0に指定すること(それぞれのAction実行毎にmp消費判定が走る)
    /// TODO:Actions の ARangeが違う場合の処理
    /// </summary>
    class ActionSeq : Action
    {
        //Seqの攻撃対象どうしようね
        List<Action> Actions;
        public ActionSeq(List<Action> actions, Range.AttackRange aRange, int mp = 0)
            : base(aRange, mp) => Actions = actions;

        public ActionSeq GenerateActionSeq(Range.AttackRange aRange)
        {
            var tmp = (ActionSeq)MemberwiseClone();
            tmp.ARange = aRange;
            tmp.Actions = tmp.Actions.Select<Action, Action>(act =>
            {
                switch (act)
                {
                    case Attack atk:
                        return atk.GenerateAttack(aRange);
                    case Buff buf:
                        return buf.GenerateBuff(aRange);
                    case Heal heal:
                        return heal.GenerateHeal(aRange);
                }
                throw new Exception("not implemented");
            }).ToList();
            return tmp;
        }

        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            cEffects = base.CollectConditionedEffects(cEffects);

            var cEfs = Actions.Aggregate(cEffects, (cefs, act) => act.CollectConditionedEffects(cefs));
            return cEfs;
        }
    }



}
