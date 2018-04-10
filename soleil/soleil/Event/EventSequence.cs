using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class EventSequence
        :IAggregate
    {
        bool startEvent;
        EventSet[] eventSets;
        int index;
        public EventSequence(params EventSet[] _eventSets)
        {
            eventSets = _eventSets;
            for (int i = 0; i < eventSets.Length; i++)
            {
                eventSets[i].SetEventSequence(this);
            }
        }

        public void Update()
        {
            if (startEvent) Execute();
        }

        public void StartEvent()
        {
            index = 0;
            startEvent = true;
            for (int i = 0; i < eventSets.Length; i++)
            {
                eventSets[i].Reset();
            }
        }

        /// <summary>
        /// EventSetから呼び出される. EventSetが終了したときに次のEventSetに進める.
        /// </summary>
        public void Next()
        {
            if(++index >= eventSets.Length)
            {
                EndEvent();
                return;
            }
        }

        private void EndEvent()
        {
            startEvent = false;
        }

        private void Execute()
        {
            Console.WriteLine(index + ":" + eventSets.Length);
            eventSets[index].Execute();
        }



        // Iterator
        public IIterator Iterator()
        {
            return new EventSequenceIterator(this);
        }

        public EventSet GetEventSet(int _index)
        {
            return eventSets[_index];
        }

        public int GetLength() => eventSets.Length;
    }
}
