using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class BoolSet
    {
        private bool[] bools;
        public int Length => bools.Length;
        public bool this[int i]
        {
            set => bools[i] = value;
            get => bools[i];
        }

        public BoolSet(int length)
        {
            bools = new bool[length];
        }
    }
}
