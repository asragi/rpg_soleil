using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    abstract class EventUnit
    {
        public abstract void Reset();

        public static EventSet[] Unit2Set(EventUnit[] units)
        {
            List<EventSet> list = new List<EventSet>();
            List<EventBase> events = new List<EventBase>();
            foreach (var item in units)
            {
                if (item is EventSet es)
                {
                    if (events.Count > 0)
                    {
                        list.Add(new EventSet(events.ToArray()));
                        events = new List<EventBase>();
                    }
                    list.Add(es);
                    continue;
                }
                events.Add((EventBase)item);
            }
            if (events.Count > 0) list.Add(new EventSet(events.ToArray()));
            return list.ToArray();
        }
    }
}
