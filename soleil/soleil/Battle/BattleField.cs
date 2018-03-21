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
        List<int>[] campIndex;
        MagicField magicField;
        TurnQueue turnQueue;

        /// <summary>
        /// turnQueueにPushされていない最初のTurn
        /// </summary>
        List<Turn> lastTurn;

        public BattleField()
        {
            campIndex = new List<int>[(int)Side.Size];
            for (int i = 0; i < 2; i++)
                campIndex[i] = new List<int>();

            charas.Add(new Character(this));
            charas.Add(new Character(this));
            charas.Add(new Character(this));
            charas.Add(new Character(this));
            charas.Add(new Character(this));

            magicField = new SimpleMagicField();
            turnQueue = new TurnQueue();

            for (int i = 0; i < charas.Count; i++)
                lastTurn.Add(charas[i].NextTurn());
            while(!turnQueue.IsFulfilled())
                EnqueueTurn();
        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);

        bool select = false;
        enum Command
        {
            Attack,
            Magic,
            Item,
            Escape,
            Size,
        }
        Command selectCommand;
        public void Move()
        {

            if(select)
            {
                if (KeyInput.GetKeyPush(Key.Down))
                {
                    selectCommand++;
                    if (selectCommand == Command.Size)
                    {
                        selectCommand = Command.Attack;
                    }
                }
                if (KeyInput.GetKeyPush(Key.Up))
                {
                    if (selectCommand == Command.Attack)
                    {
                        selectCommand = Command.Size;
                    }
                    selectCommand--;
                }
                if (KeyInput.GetKeyPush(Key.A))
                {

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
            var minIndex = lastTurn.FindMin(p => p.TurnTime).Index;
            turnQueue.Push(lastTurn[minIndex]);
            lastTurn[minIndex] = charas[minIndex].NextTurn();
        }

        public void Draw()
        {

        }
    }
}
