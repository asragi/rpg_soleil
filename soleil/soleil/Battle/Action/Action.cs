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


    abstract class Action
    {
        public Range.AttackRange ARange
        {
            get; protected set;
        }
        public Action(Range.AttackRange aRange)
        {
            ARange = aRange;
        }

        public abstract List<Occurence> Act(BattleField battle);


        public List<Occurence> AggregateConditionEffects(BattleField bf, IEnumerable<ConditionedEffect> additionals, List<Occurence> ocr)
        {
            var ceffects = bf.GetCopiedCEffects();
            ceffects.UnionWith(additionals);
            return ceffects.Aggregate(ocr, (ocrs, ce) => ce.Act(bf, this, ocrs));
        }
        public List<Occurence> AggregateConditionEffects(BattleField bf, IEnumerable<ConditionedEffect> additionals)
        {
            return AggregateConditionEffects(bf, additionals, new List<Occurence>());
        }
    }


    
}
