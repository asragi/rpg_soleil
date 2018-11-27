using Soleil.Map;
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
        EventBase[] eventsDefault;
        List<EventBase> events;
        int index;

        public EventSet(params EventBase[] _events)
        {
            eventsDefault = _events;
            Reset();
        }

        public void Reset()
        {
            index = 0;
            events = new List<EventBase>();
            for (int i = 0; i < eventsDefault.Length; i++)
            {
                events.Add(eventsDefault[i]);
                AddExtraEvents(events, eventsDefault[i]);
            }
            for (int i = 0; i < events.Count; i++)
            {
                events[i].SetEventSet(this);
                events[i].Reset();
            }
        }

        public void SetReference(ObjectManager _om)
        {
            for (int i = 0; i < eventsDefault.Length; i++)
            {
                eventsDefault[i].SetReference(_om);
            }
        }

        private void AddExtraEvents(List<EventBase> list, EventBase e)
        {
            switch (e)
            {
                default:
                    break;
            }
        }


        public void SetEventSequence(EventSequence es)
        {
            myEventSequence = es;
        }

        public virtual void Execute()
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

        public void Draw(Drawing d)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].Draw(d);
            }
        }
    }
}
