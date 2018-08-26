﻿using System;
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
        MagicField magicField;
        TurnQueue turnQueue;

        /// <summary>
        /// turnQueueにPushされていない最初のTurn
        /// </summary>
        List<Turn> lastTurn;
        List<BattleUI> UIList;

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

            magicField = new SimpleMagicField();
            turnQueue = new TurnQueue();

            lastTurn = new List<Turn>();
            for (int i = 0; i < charas.Count; i++)
                lastTurn.Add(charas[i].NextTurn());
            while (!turnQueue.IsFulfilled())
                EnqueueTurn();

            battleQue = new Queue<BattleEvent>();

            UIList = new List<BattleUI>();
        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);

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

        /// <summary>
        /// 行動するCharacterのIndex
        /// </summary>

        Turn topTurn;

        Queue<BattleEvent> battleQue;
        BattleEvent beTop;
        int delayCount = 0;
        bool executed = true;
        public void Move()
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
                    foreach (var ocr in ocrs)
                        ExecOccurence(ocr);

                    EnqueueTurn();
                    ocrs.ForEach(ocr => battleQue.Enqueue(new BattleMessage(ocr.Message, 60)));
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
                    var action = charas[topTurn.CharaIndex].SelectAction();
                    if (action != null)
                    {
                        executed = true;
                        turnQueue.Push(new ActionTurn(topTurn.WaitPoint + 100, topTurn.SPD, topTurn.CharaIndex, action));
                        delayCount = 0;
                    }
                    break;
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
            ocr.Affect(this);
        }

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
                sb.DrawText(new Vector(150 + i * 150, 400), Resources.GetFont(FontID.Test), i.ToString() + ":", Color.White, DepthID.Message);
                sb.DrawText(new Vector(150 + i * 150, 440), Resources.GetFont(FontID.Test), charas[i].Status.HP.ToString() + "/" + charas[i].Status.abilityScore.HPMAX.ToString(), Color.White, DepthID.Message);
            }

            //sb.DrawBox(new Vector(20, 400), new Vector(20,20), Color.White, DepthID.Message);

            UIList.ForEach(e => e.Draw(sb));
        }
    }
}
