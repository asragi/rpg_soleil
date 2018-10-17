using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
        List<Side> sides;
        List<int>[] indexes;
        List<Character> charas;
        List<bool> alive;

        MagicField magicField;
        TurnQueue turnQueue;

        /// <summary>
        /// turnQueueにPushされていない最初のTurn
        /// </summary>
        List<Turn> lastTurn;
        List<BattleUI> UIList;

        public SortedSet<ConditionedEffect> CEffects;
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

            sides = new List<Side>
            {
                Side.Right,
                Side.Right,
                Side.Left,
                Side.Left,
                Side.Left,
            };
            indexes = new List<int>[(int)Side.Size];
            indexes[(int)Side.Left] = new List<int> { 2, 3, 4, };
            indexes[(int)Side.Right] = new List<int> { 0, 1, };
            alive = new List<bool>(charas.Count);
            for (int i = 0; i < charas.Count; i++) alive.Add(true);

            magicField = new SimpleMagicField();
            turnQueue = new TurnQueue();

            lastTurn = new List<Turn>();
            for (int i = 0; i < charas.Count; i++)
                lastTurn.Add(charas[i].NextTurn());
            while (!turnQueue.IsFulfilled())
                EnqueueTurn();

            battleQue = new Queue<BattleEvent>();

            UIList = new List<BattleUI>();
            CEffects = new SortedSet<ConditionedEffect>();
        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);

        //shallow copy
        public SortedSet<ConditionedEffect> GetCopiedCEffects()
            => (SortedSet<ConditionedEffect>)CEffects.Select(p => p);

        public Character GetCharacter(int index) => charas[index];

        public Side OppositeSide(Side side)
        {
            switch (side)
            {
                case Side.Right:
                    return Side.Left;
                case Side.Left:
                    return Side.Right;
                default:
                    return Side.Size;
            }
        }
        public List<int> OppositeIndexes(int index) => indexes[(int)OppositeSide(sides[index])];

        public void RemoveCharacter(int index)
        {
            var sd = sides[index];
            indexes[(int)sd].Remove(index);
            //charas[index] = null;
            turnQueue.RemoveAll(p => p.CharaIndex == index);
            alive[index] = false;
            while (!turnQueue.IsFulfilled())
                EnqueueTurn();
        }

        Turn topTurn;

        Queue<BattleEvent> battleQue;
        BattleEvent beTop;
        int delayCount = 0;
        bool executed = true;
        public void Update()
        {
            if (delayCount > 0)
            {
                delayCount--;
            }

            if (battleQue.Count == 0 && executed)
            {
                topTurn = turnQueue.Top();
                turnQueue.Pop();

                //Turnが行動実行Turnのとき
                if (topTurn is ActionTurn actTurn)
                {
                    //行動を実行
                    var ocrs = actTurn.action.Act(this);

                    //TODO:Occurenceに応じたBattleEventを生成する
                    ocrs.ForEach(ocr => battleQue.Enqueue(new BattleMessage(ocr.Message, 60)));
                    EnqueueTurn();
                }
                //Turnが行動選択Turnのとき
                else
                {
                    battleQue.Enqueue(new BattleCommandSelect(topTurn.CharaIndex, -1));
                }
            }

            if (delayCount == 0)
            {
                beTop = battleQue.Dequeue();
                delayCount = beTop.DequeCount;
            }
            switch (beTop)
            {
                case BattleMessage bm:
                    message = bm.Message;
                    executed = true;
                    break;
                case BattleCommandSelect bcs:
                    executed = false;
                    var action = charas[topTurn.CharaIndex].SelectAction(topTurn);
                    if (action)
                    {
                        executed = true;
                        delayCount = 0;
                    }
                    break;
            }

        }

        public void EnqueueTurn(Turn turn) => turnQueue.Push(turn);
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
            var minIndex = lastTurn.Where(p => alive[p.CharaIndex])
                .FindMin(p => p.TurnTime).CharaIndex;
            turnQueue.Push(lastTurn[minIndex]);
            lastTurn[minIndex] = charas[minIndex].NextTurn();
        }

        /*
        void ExecOccurence(Occurence ocr)
        {
            ocr.Affect(this);
        }
        */

        public void AddUI(BattleUI bui) => UIList.Add(bui);
        public bool RemoveUI(BattleUI bui) => UIList.Remove(bui);

        string message = "";
        public void Draw(Drawing sb)
        {
            //てきとう
            sb.DrawText(new Vector(300, 100), Resources.GetFont(FontID.Test), message, Color.White, DepthID.Message);

            /*
            sb.DrawText(new Vector(100, 400), Resources.GetFont(FontID.Test), "Magic", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 440), Resources.GetFont(FontID.Test), "Skill", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 480), Resources.GetFont(FontID.Test), "Guard", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 520), Resources.GetFont(FontID.Test), "Escape", Color.White, DepthID.Message);
            */

            sb.DrawText(new Vector(400, 50), Resources.GetFont(FontID.Test), topTurn.CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            for (int i = 0; i < turnQueue.Count; i++)
                sb.DrawText(new Vector(510 + i * 110, 50), Resources.GetFont(FontID.Test), turnQueue[i].CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);


            for (int i = 0; i < charas.Count; i++)
            {
                sb.DrawText(new Vector(100 + i * 180, 400), Resources.GetFont(FontID.Test), i.ToString() + ":", Color.White, DepthID.Message);
                //TODO:表示するステータスはchara[i].Statusから分離する
                sb.DrawText(new Vector(100 + i * 180, 440), Resources.GetFont(FontID.Test), charas[i].Status.HP.ToString() + "/" + charas[i].Status.AScore.HPMAX.ToString(), Color.White, DepthID.Message, 0.75f);
            }

            //sb.DrawBox(new Vector(20, 400), new Vector(20,20), Color.White, DepthID.Message);

            UIList.ForEach(e => e.Draw(sb));
        }
    }
}
