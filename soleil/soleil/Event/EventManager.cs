using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class EventManager
    {
        bool startEvent;
        List<EventBase> events;
        int index;

        public EventManager()
        {
            events = new List<EventBase>();
            startEvent = false;
            index = 0;
        }

        public void AddEvent(EventBase e)
        {
            events.Add(e);
            switch (e)
            {
                case MessageWindowEvent mwe:
                    new MessageUpdateEvent(mwe.Tag, this);
                    new WindowCloseEvent(mwe.Tag, this);
                    break;
                default:
                    break;
            }
        }

        public void StartEvent()
        {
            index = 0;
            startEvent = true;
        }
        public void NextIndex()
        {
            if(index >= events.Count-1)
            {
                startEvent = false;
                return;
            }
            index++;
        }

        public void Update()
        {
            Console.WriteLine(index);
            NowUpdate();
        }

        private void NowUpdate()
        {
            if (!startEvent) return;
            events[index].Execute();
        }
    }
}
