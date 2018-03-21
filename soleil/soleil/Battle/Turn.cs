using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TurnQueue
    {
        const int Length = 7;
        List<Turn> queue;
        public TurnQueue()
        {
            queue = new List<Turn>();
        }

        public void Update()
        {
            queue.Sort((x,y)=> {
                if (x.TurnTime > y.TurnTime) return 1;
                else if (x.TurnTime < y.TurnTime) return -1;
                else return 0;
            });
            for (int i = 0; i < queue.Count; i++)
            {
                queue[i].Index = i;
            }
        }

        public void Pop()
        {
            queue.RemoveAt(0);
            Update();
        }

        public void Push(Turn turn)
        {
            queue.Add(turn);
            Update();
        }

        public void PushAll(List<Turn> turns)
        {
            queue.AddRange(turns);
            Update();
        }
    }
    class Turn
    {
        public int Index;
        public Side Side;
        public int WaitPoint;
        public int SPD;
        
        /// <summary>
        /// WaitPointが0以下になるのにかかる時間
        /// </summary>
        public float TurnTime
        {
            get { return WaitPoint / SPD; }
        }

        public Turn()
        {
        }
    }
}
