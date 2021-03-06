﻿using System;

namespace Soleil.Event
{
    class BoolEventBranch
        : EventBranch
    {
        EventUnit[] trueSet, falseSet;
        Func<bool> condition;
        public BoolEventBranch(EventSequence es, Func<bool> _condition, EventUnit[] _trueSet, EventUnit[] _falseSet)
            : base(es)
        {
            trueSet = _trueSet;
            falseSet = _falseSet;
            condition = _condition;
        }

        public BoolEventBranch(EventSequence es, Func<bool> _condition, EventUnit _trueSet, EventUnit _falseSet)
            : this(es, _condition, new[] { _trueSet }, new[] { _falseSet }) { }

        protected override EventUnit[] DecideEventSet()
        {
            return (condition()) ? trueSet : falseSet;
        }
    }
}
