using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// 戦闘中のEvent
    /// Turnに応じて生成される
    /// </summary>
    abstract class BattleEvent
    {
        public int DequeCount { get; private set; }
        public BattleEvent(int dequeCount) => DequeCount = dequeCount;
    }

    class BattleMessage : BattleEvent
    {
        public string Message { get; private set; }
        public BattleMessage(string message, int dequeCount) : base(dequeCount)
        {
            Message = message;
        }
    }

    class BattleCommandSelect : BattleEvent
    {
        public int CharaIndex { get; private set; }
        public BattleCommandSelect(int charaIndex, int dequeCount) : base(dequeCount)
        {
            CharaIndex = charaIndex;
        }
    }

    class BattleEnd : BattleEvent
    {
        public bool DidWin;
        public BattleEnd(int dequeCount, bool didWin) : base(dequeCount) => DidWin = didWin;
    }

    class BattleEffect : BattleEvent
    {
        public Occurence Occur;
        public BattleEffect(Occurence occurence) : base(occurence.Time)
        {
            this.Occur = occurence;
        }

        public void Act() =>
            Occur.Affect();
    }
}
