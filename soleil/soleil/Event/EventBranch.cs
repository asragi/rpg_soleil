namespace Soleil.Event
{
    /// <summary>
    /// 条件に応じてEventSequenceのEventSetListに新しくEventSetを挿入する。
    /// </summary>
    abstract class EventBranch : EventSet
    {
        protected EventSequence MySequence;
        public EventBranch(EventSequence e)
            : base()
        {
            MySequence = e;
        }
    }
}
