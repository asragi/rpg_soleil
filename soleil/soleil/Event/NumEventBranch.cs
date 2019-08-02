using System;

namespace Soleil.Event
{
    class NumEventBranch
        : EventBranch
    {
        EventUnit[][] eventSets;
        Func<int> index;
        public NumEventBranch(EventSequence es, Func<int> _index, params EventUnit[][] _eventSets)
            : base(es)
        {
            index = _index;
            eventSets = _eventSets;
        }

        protected override EventUnit[] DecideEventSet()
        {
            return eventSets[index()];
        }
    }
}