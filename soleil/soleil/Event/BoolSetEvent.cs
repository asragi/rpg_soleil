using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// BoolSetを受け取って値を変更する．
    /// </summary>
    class BoolSetEvent : EventBase
    {
        BoolSet boolSet;
        bool value;
        int target;

        public BoolSetEvent(BoolSet bools, int _target, bool val)
        {
            (boolSet, target, value) = (bools, _target, val);
        }

        public override void Execute()
        {
            base.Execute();
            boolSet[target] = value;
            Next();
        }
    }
}
