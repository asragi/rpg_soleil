using System;

namespace Soleil.Event
{
    class BoolEventBranch
        :EventBranch
    {
        EventSet trueSet, falseSet;
        Func<bool> condition;
        public BoolEventBranch(EventSequence es, Func<bool> _condition, EventSet _trueSet, EventSet _falseSet)
            :base(es)
        {
            trueSet = _trueSet;
            falseSet = _falseSet;
            condition = _condition;
        }

        protected override EventSet DecideEventSet()
        {
            return (condition()) ? trueSet : falseSet;
        }
    }
}
