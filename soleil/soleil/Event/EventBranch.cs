using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    static class EventBranch
    {
        public static EventSet BoolEventBranch(bool condition, EventSet trueSet, EventSet falseSet)
        {
            return condition ? trueSet : falseSet;
        }

        public static EventSet NumEventBranch(int num, params EventSet[] eventSets)
        {
            return eventSets[num];
        }
    }
}
