namespace Soleil.Event
{
    /// <summary>
    /// 条件に応じてEventSequenceのEventSetListに新しくEventSetを挿入する。
    /// </summary>
    abstract class EventBranch : EventSet
    {
        protected EventSequence MySequence;
        protected EventSet decidedSet;
        public EventBranch(EventSequence e)
            : base()
        {
            MySequence = e;
        }

        public override void Execute()
        {
            int index = MySequence.GetNowIndex();
            decidedSet = DecideEventSet();
            decidedSet.Reset();
            MySequence.InsertEventSet(index + 1, decidedSet);
            Next();
        }

        protected abstract EventSet DecideEventSet();
    }
}
