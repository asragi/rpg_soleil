using System.Collections.Generic;

namespace Soleil.Event
{
    /// <summary>
    /// 条件に応じてEventSequenceのEventSetListに新しくEventSetを挿入する。
    /// </summary>
    abstract class EventBranch : EventSet
    {
        protected EventSequence MySequence;
        protected EventUnit[] decidedSet;
        public EventBranch(EventSequence e)
            : base()
        {
            MySequence = e;
        }

        public override void Execute()
        {
            int index = MySequence.GetNowIndex();
            decidedSet = DecideEventSet();
            decidedSet.ForEach2(s => s.Reset());

            // 気合
            var createdSet = new List<EventBase>();
            for (int i = decidedSet.Length - 1; i >= 0; --i)
            {
                var target = decidedSet[i];
                if (target is EventSet eSet)
                {
                    if(createdSet.Count > 0)
                    {
                        MySequence.InsertEventSet(index + 1, new EventSet(createdSet.ToArray()));
                        createdSet = new List<EventBase>();
                    }
                    MySequence.InsertEventSet(index + 1, eSet);
                    continue;
                }
                var eBase = (EventBase)target;
                createdSet.Add(eBase);
            }

            if (createdSet.Count > 0) MySequence.InsertEventSet(index + 1, new EventSet(createdSet.ToArray()));
            Next();
        }

        protected abstract EventUnit[] DecideEventSet();
    }
}
