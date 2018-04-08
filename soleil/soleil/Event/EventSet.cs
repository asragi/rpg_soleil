using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// Event管理用 Eventのまとまりを表すクラス.
    /// </summary>
    class EventSet
    {
        EventSequence myEventSequence;
        List<EventBase> events;
        int index;

        public EventSet(params EventBase[] _events)
        {
            index = 0;
            events = new List<EventBase>();
            for (int i = 0; i < _events.Length; i++)
            {
                events.Add(_events[i]);
                AddExtraEvents(events, _events[i]);
            }
            for (int i = 0; i < events.Count; i++)
            {
                events[i].SetEventSet(this);
            }
        }

        public void Reset()
        {
            index = 0;
        }

        private void AddExtraEvents(List<EventBase> list, EventBase e)
        {
            switch (e)
            {
                case MessageWindowEvent mwe:
                    list.Add(new ChangeInputFocusEvent(InputFocus.Window));
                    list.Add(new MessageUpdateEvent(mwe.Tag));
                    list.Add(new WindowCloseEvent(mwe.Tag));
                    break;
                default:
                    break;
            }
        }


        public void SetEventSequence(EventSequence es)
        {
            myEventSequence = es;
        }

        public void Execute()
        {
            events[index].Execute();
        }


        /// <summary>
        /// EventBaseから呼び出される. EventBaseが終了したときに次のEventBaseに進める.
        /// </summary>
        public void Next()
        {
            if(++index >= events.Count)
            {
                myEventSequence.Next();
                return;
            }
        }



    }
}
