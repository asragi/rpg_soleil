using Soleil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class EventSequenceIterator
        :IIterator
    {
        private EventSequence eventSequence;
        private int index;

        public EventSequenceIterator(EventSequence es)
        {
            eventSequence = es;
            index = 0;
        }

        public bool HasNext()
        {
            return index < eventSequence.GetLength();
        }

        public Object Next()
        {
            var eventSet = eventSequence.GetEventSet(index);
            index++;
            return eventSet;
        }
    }
}
