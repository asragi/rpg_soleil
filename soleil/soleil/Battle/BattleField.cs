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
        static readonly BattleField singleton = new BattleField();
        public static BattleField GetInstance() => singleton;

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
        List<Menu.MenuComponent> MenuComponentList;

        List<TextureID> textureIDList;
        //debugでpublic
        public List<StatusUI> statusUIs;

        public SortedSet<ConditionedEffect> CEffects;
        public BattleField()
        {
        }

        public void InitBattle()
        {
            MenuComponentList = new List<Menu.MenuComponent>();

            charas = new List<Character>
            {
                new TestPlayableCharacter(0),
                new TestPlayableCharacter(1),
                new TestEnemyCharacter(2),
                new TestEnemyCharacter(3),
                new TestEnemyCharacter(4),
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

            CEffects = new SortedSet<ConditionedEffect>();

            textureIDList = new List<TextureID>
            {
                TextureID.BattleTurnQueueFaceLune,
                TextureID.BattleTurnQueueFaceSun,
                TextureID.BattleTurnQueueFace1,
                TextureID.BattleTurnQueueFace2,
                TextureID.BattleTurnQueueFace3,
            };

            statusUIs = new List<StatusUI>()
            {
                new StatusUI(charas[0].Status.HP, charas[1].Status.HP, new Vector(550, 450)),
                new StatusUI(charas[0].Status.HP, charas[1].Status.HP, new Vector(750, 450)),
            };
        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);

        //shallow copy
        public SortedSet<ConditionedEffect> GetCopiedCEffects()
            => new SortedSet<ConditionedEffect>(CEffects);
        public void AddCEffect(ConditionedEffect cEffect) => CEffects.Add(cEffect);

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
        public List<int> OppositeIndexes(Side side) => indexes[(int)OppositeSide(side)];
        public List<int> SameSideIndexes(int index) => indexes[(int)sides[index]];
        public List<int> SameSideIndexes(Side side) => indexes[(int)side];
        public List<int> AliveIndexes()
            => alive.Aggregate2(new List<int>(), (list, p, i) => {
                if (p) list.Add(i);
                return list;
            });

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
            MenuComponentList.ForEach(e => e.Update());
            if (delayCount > 0)
            {
                delayCount--;
            }

            if (battleQue.Count == 0 && executed)
            {
                while(turnQueue.Top().TurnTime > 0)
                {
                    charas.ForEach(e => e.Status.WP += e.Status.SPD);
                }
                topTurn = turnQueue.Top();
                turnQueue.Pop();

                CEffects.RemoveWhere(e => e.Expired());

                //Turnが行動実行Turnのとき
                if (topTurn is ActionTurn actTurn)
                {
                    //行動を実行
                    var ocrs = actTurn.action.Act();

                    //TODO:Occurenceに応じたBattleEventを生成する
                    ocrs.ForEach(e => e.Affect());
                    ocrs.ForEach(ocr => battleQue.Enqueue(new BattleMessage(ocr.Message, 60)));
                }
                //Turnが行動選択Turnのとき
                else
                {
                    battleQue.Enqueue(new BattleCommandSelect(topTurn.CharaIndex, -1));
                    EnqueueTurn();
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
                    executed = delayCount <= 1;
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

            statusUIs.ForEach(e => e.Update());
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
        
        public void AddBasicMenu(Menu.MenuComponent bui) => MenuComponentList.Add(bui);
        public bool RemoveBasicMenu(Menu.MenuComponent bui) => MenuComponentList.Remove(bui);

        string message = "";
        const int TurnQueueTextureWidth = 80;
        public void Draw(Drawing sb)
        {
            sb.Draw(new Vector(Game1.VirtualCenterX, Game1.VirtualCenterY), Resources.GetTexture(TextureID.BattleTemporaryBackground), DepthID.BackGround);

            //てきとう
            sb.DrawText(new Vector(300, 100), Resources.GetFont(FontID.CorpM), message, Color.White, DepthID.Message);

            /*
            sb.DrawText(new Vector(100, 400), Resources.GetFont(FontID.Yasashisa), "Magic", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 440), Resources.GetFont(FontID.Yasashisa), "Skill", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 480), Resources.GetFont(FontID.Yasashisa), "Guard", Color.White, DepthID.Message);
            sb.DrawText(new Vector(100, 520), Resources.GetFont(FontID.Yasashisa), "Escape", Color.White, DepthID.Message);
            */

            sb.DrawText(new Vector(400, 50), Resources.GetFont(FontID.CorpM), topTurn.CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            for (int i = 0; i < turnQueue.Count; i++)
                sb.DrawText(new Vector(510 + i * 110, 50), Resources.GetFont(FontID.CorpM), turnQueue[i].CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            sb.Draw(new Vector(450, 50), Resources.GetTexture(textureIDList[topTurn.CharaIndex]), DepthID.MenuTop);
            for (int i=0;i<5;i++)
                sb.Draw(new Vector(600 + i * TurnQueueTextureWidth, 50), Resources.GetTexture(textureIDList[turnQueue[i].CharaIndex]), DepthID.MenuTop);


            statusUIs.ForEach(e => e.Draw(sb));
            for (int i = 2; i < charas.Count; i++)
            {
                sb.DrawText(new Vector(100 + i * 180, 400), Resources.GetFont(FontID.CorpM), i.ToString() + ":", Color.White, DepthID.Message);
                sb.DrawText(new Vector(100 + (i-2) * 180, 400), Resources.GetFont(FontID.Test), i.ToString() + ":", Color.Black, DepthID.Message);
                //TODO:表示するステータスはchara[i].Statusから分離する
                sb.DrawText(new Vector(100 + i * 180, 440), Resources.GetFont(FontID.CorpM), charas[i].Status.HP.ToString() + "/" + charas[i].Status.AScore.HPMAX.ToString(), Color.White, DepthID.Message, 0.75f);
                sb.DrawText(new Vector(100 + (i-2) * 180, 440), Resources.GetFont(FontID.Test), charas[i].Status.HP.ToString() + "/" + charas[i].Status.AScore.HPMAX.ToString(), Color.Black, DepthID.Message, 0.75f);
            }

            
            MenuComponentList.ForEach(e => e.Draw(sb));
        }
    }
}
