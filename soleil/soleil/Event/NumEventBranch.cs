using System;

namespace Soleil.Event
{
    class NumEventBranch
        : EventBranch
    {
        EventSet[] eventSets;
        Func<int> index;
        public NumEventBranch(EventSequence es, Func<int> _index, params EventSet[] _eventSets)
            : base(es)
        {
            index = _index;
            eventSets = _eventSets;
        }

        protected override EventSet DecideEventSet()
        {
            return eventSets[index()];
        }
    }
}