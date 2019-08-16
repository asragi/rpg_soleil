using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// Turnの順序を管理する
    /// </summary>
    class TurnQueue
    {
        public const int Length = 7;
        List<Turn> queue;
        public TurnQueue()
        {
            queue = new List<Turn>();
        }

        public void Refresh()
        {
            queue.Sort((x, y) =>
            {
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
            Refresh();
            return top;
        }

        public void Push(Turn turn)
        {
            queue.Add(turn);
            Refresh();
        }

        public void PushAll(List<Turn> turns)
        {
            queue.AddRange(turns);
            Refresh();
        }

        public Turn this[int index]
        {
            get
            {
                return queue[index];
            }
            set
            {
                queue[index] = value;
            }
        }
        public int Count => queue.Count;
        public int RemoveAll(Predicate<Turn> f) => queue.RemoveAll(f);

        public bool IsFulfilled() => Length <= queue.Count;
    }


    class Turn
    {
        public int Index = -1;
        public int WaitPoint;
        public CharacterStatus CStatus;
        public int CharaIndex;

        /// <summary>
        /// WaitPointが0以下になるのにかかる時間
        /// </summary>
        public int TurnTime
        {
            get { return (WaitPoint - CStatus.WP) / CStatus.SPD; }
        }

        public Turn(int _WaitPoint, CharacterStatus _CStatus, int _CharaIndex) => (WaitPoint, CStatus, CharaIndex) = (_WaitPoint, _CStatus, _CharaIndex);
    }

    /// <summary>
    /// 行動をするTurn
    /// </summary>
    class ActionTurn : Turn
    {
        public Action action;
        public ActionTurn(int _WaitPoint, CharacterStatus _CStatus, int _CharaIndex, Action _action) : base(_WaitPoint, _CStatus, _CharaIndex) => action = _action;
    }
}
