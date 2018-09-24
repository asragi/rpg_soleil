namespace Soleil.Event
{
    /// <summary>
    /// Event類の基底クラス
    /// </summary>
    abstract class EventBase
    {
        //protected EventManager EventManager;
        EventSet myEventSet;
        public EventBase()
        {

        }

        public void SetEventSet(EventSet es)
        {
            myEventSet = es;
        }

        /// <summary>
        /// 実行時関数. EventManagerのUpdateにより毎フレーム呼び出される.処理が終わったらNextを呼び出すこと.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 現在の処理を終了し次の処理に移る.
        /// </summary>
        protected void Next()
        {
            myEventSet.Next();
        }
    }
}
