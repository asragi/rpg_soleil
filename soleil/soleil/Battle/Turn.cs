using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TurnQueue
    {
        const int Length = 5;
        List<Turn> queue;
        public TurnQueue()
        {
            queue = new List<Turn>();
        }

        public void Update()
        {

        }
    }
    class Turn
    {
        public int Index;
        public Side Side;
        public int TurnCount;
        public Turn(int index_, Side side_)
        {
            Index = index_;
            Side = side_;
        }
    }
}
