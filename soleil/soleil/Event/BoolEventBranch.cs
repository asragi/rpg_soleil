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

        public override void Execute()
        {
            int index = MySequence.GetNowIndex();
            var item = (condition()) ? trueSet : falseSet;
            item.Reset();
            MySequence.InsertEventSet(index+1, item);
            Next();
        }
    }
}
