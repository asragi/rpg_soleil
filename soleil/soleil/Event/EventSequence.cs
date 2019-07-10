using Soleil.Map;
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
        List<EventSet> eventSets;
        EventSet[] eventSetsDefault; // 引数を保存する。
        int index;
        PlayerObject player;
        public EventSequence(PlayerObject pl)
        {
            index = 0;
            eventSets = new List<EventSet>();
            player = pl;
        }

        public void SetEventSet(params EventUnit[] _eventSets)
        {
            eventSetsDefault = EventUnit.Unit2Set(_eventSets);
            SetEventSetsByDefault();
        }

        private void SetEventSetsByDefault()
        {
            eventSets = eventSetsDefault.ToList<EventSet>();
            for (int i = 0; i < eventSets.Count; i++)
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
            SetEventSetsByDefault();
            startEvent = true;
            for (int i = 0; i < eventSets.Count; i++)
            {
                eventSets[i].Reset();
            }
        }

        /// <summary>
        /// EventSetから呼び出される. EventSetが終了したときに次のEventSetに進める.
        /// </summary>
        public void Next()
        {
            if(++index >= eventSets.Count)
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
            eventSets[index].Execute();
        }

        public int GetNowIndex() => index;

        public void InsertEventSet(int index,EventSet item)
        {
            item.SetEventSequence(this);
            eventSets.Insert(index, item);
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

        public int GetLength() => eventSets.Count;

        public void Draw(Drawing d)
        {
            for (int i = 0; i < eventSets.Count; i++)
            {
                eventSets[i].Draw(d);
            }
        }
    }
}
