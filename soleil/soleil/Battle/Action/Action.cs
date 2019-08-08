using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil
{

    enum ActionName
    {
        //Attack
        NormalAttack,
        ExampleMagic,

        //Buff
        Guard,
        EndGuard,
        ExampleDebuff,

        Size,
    }


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

        public abstract List<Occurence> Act();
        protected static readonly BattleField BF = BattleField.GetInstance();


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
