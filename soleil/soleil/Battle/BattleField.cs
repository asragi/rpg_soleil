using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum Side
    {
        Left,
        Right,
        Size,
    }
    class BattleField
    {
        List<Character> charas;
        MagicField magicField;
        TurnQueue turnQueue;

        /// <summary>
        /// turnQueueにPushされていない最初のTurn
        /// </summary>
        List<Turn> lastTurn;

        public BattleField()
        {
            charas = new List<Character>
            {
                new TestPlayableCharacter(this, 0),
                new TestPlayableCharacter(this, 1),
                new TestEnemyCharacter(this, 2),
                new TestEnemyCharacter(this, 3),
                new TestEnemyCharacter(this, 4),
            };

            magicField = new SimpleMagicField();
            turnQueue = new TurnQueue();

            lastTurn = new List<Turn>();
            for (int i = 0; i < charas.Count; i++)
                lastTurn.Add(charas[i].NextTurn());
            while(!turnQueue.IsFulfilled())
                EnqueueTurn();
        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);

        public Character GetCharacter(int index) => charas[index];

        /// <summary>
        /// 行動するCharacterのIndex
        /// </summary>

        bool turnUpdate = true;
        Turn topTurn;
        public void Move()
        {
            if(turnUpdate)
            {
                topTurn = turnQueue.Top();
                turnUpdate = false;
            }

            //Turnが行動実行Turnのとき
            if (topTurn is ActionTurn actTurn)
            {
                //行動を実行
                var ocrs = actTurn.action.Act(this);
                foreach(var ocr in ocrs)
                    ExecOccurence(ocr);

                turnQueue.Pop();
                EnqueueTurn();
                turnUpdate = true;
            }
            //Turnが行動選択Turnのとき
            else
            {
                var action = charas[topTurn.CharaIndex].SelectAction();
                if(action != null)
                {
                    turnQueue.Pop();
                    turnQueue.Push(new ActionTurn(topTurn.WaitPoint + 100, topTurn.SPD, topTurn.Index, action));
                    turnUpdate = true;
                }
            }
        }

        void EnqueueTurn()
        {
            /*
            float minTT = (float)1e9;//十分に大きい数
            int minIndex =-1;
            for(int i=0;i<lastTurn.Count;i++)
                if(lastTurn[i].TurnTime < minTT)
                {
                    minTT = lastTurn[i].TurnTime;
                    minIndex = i;
                }
                */
            var minIndex = lastTurn.FindMin(p => p.TurnTime).CharaIndex;
            turnQueue.Push(lastTurn[minIndex]);
            lastTurn[minIndex] = charas[minIndex].NextTurn();
        }

        void ExecOccurence(Occurence ocr)
        {
            switch (ocr)
            {
                case OccurenceForCharacter ocrc:
                    charas[ocrc.CharaIndex].Damage(ocrc.HPDamage, ocrc.MPDamage);
                    break;
                case OccurenceForField ocrf:
                    break;
            }
        }

        public void Draw(Drawing sb)
        {

        }
    }
}
