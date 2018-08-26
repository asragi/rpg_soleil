using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TurnQueue
    {
        public const int Length = 7;
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
        public Turn Top() => queue[0];

        public Turn Pop()
        {
            var top = queue[0];
            queue.RemoveAt(0);
            Update();
            return top;
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

        public Turn this[int index]
        {
            get
            {
                return queue[index];
            }
            set
            {
                queue[index]=value;
            }
        }
        public int Count => queue.Count;

        public bool IsFulfilled() => Length <= queue.Count;
    }


    class Turn
    {
        public int Index = -1;
        public Side Side;
        public int WaitPoint;
        public Reference<int> SPD;
        public int CharaIndex;
        
        /// <summary>
        /// WaitPointが0以下になるのにかかる時間
        /// </summary>
        public float TurnTime
        {
            get { return WaitPoint / SPD.Val; }
        }

        public Turn(int _WaitPoint, Reference<int> _SPD, int _CharaIndex) => (WaitPoint, SPD, CharaIndex) = (_WaitPoint, _SPD, _CharaIndex);
    }

    class ActionTurn : Turn
    {
        public Action action;
        public ActionTurn(int _WaitPoint, Reference<int> _SPD, int _CharaIndex, Action _action) : base(_WaitPoint, _SPD, _CharaIndex) => action = _action;
    }
}
