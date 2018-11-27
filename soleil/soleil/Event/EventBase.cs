using Soleil.Map;

namespace Soleil.Event
{
    /// <summary>
    /// Event類の基底クラス
    /// </summary>
    abstract class EventBase
    {
        bool started;
        EventSet myEventSet;
        protected ObjectManager ObjectManager;
        public EventBase()
        {
            started = false;
        }

        public void SetEventSet(EventSet es)
        {
            myEventSet = es;
        }

        public void SetReference(ObjectManager om) => ObjectManager = om;

        /// <summary>
        /// 最初のフレームで実行される関数
        /// </summary>
        public virtual void Start() => started = true;

        /// <summary>
        /// 実行時関数. EventManagerのUpdateにより毎フレーム呼び出される.処理が終わったらNextを呼び出すこと.
        /// </summary>
        public virtual void Execute()
        {
            if (!started) Start();
        }

        /// <summary>
        /// 現在の処理を終了し次の処理に移る.
        /// </summary>
        protected void Next()
        {
            myEventSet.Next();
        }

        public void Reset()
        {
            started = false;
        }

        public virtual void Draw(Drawing d) { }
    }
}
