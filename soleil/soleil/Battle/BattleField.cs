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
        int charaIndex;
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

        public List<BattleCharaGraphics> bcgraphicsList;

        public SortedSet<ConditionedEffect> CEffects;
        public BattleField()
        {
        }

        public void InitBattle(PersonParty party)
        {
            MenuComponentList = new List<Menu.MenuComponent>();

            var partylist = party.GetActiveMembers();

            charaIndex = 0;
            charas = new List<Character>();
            sides = new List<Side>();
            indexes = new List<int>[(int)Side.Size] { new List<int>(), new List<int>() };
            bcgraphicsList = new List<BattleCharaGraphics>();
            textureIDList = new List<TextureID> //とりあえず
            {
                TextureID.BattleTurnQueueFaceLune,
                TextureID.BattleTurnQueueFaceSun,
                TextureID.BattleTurnQueueFaceSun,
            };
            for (int i = 0; i < partylist.Length; i++)
            {
                charas.Add(new PlayableCharacter(charaIndex, partylist[i].Score));
                sides.Add(Side.Right);
                indexes[(int)Side.Right].Add(charaIndex);
                bcgraphicsList.Add(new BattleCharaGraphics(charas.Last(), new Vector(750 - (partylist.Length - i - 1) * 200, 450), new Vector(600 + i * 50, 200 + i * 100)));
                charaIndex++;
            }

            const int EnemyCnt = 3;
            for (int i = 0; i < EnemyCnt; i++)
            {
                charas.Add(new TestEnemyCharacter(charaIndex));
                sides.Add(Side.Left);
                indexes[(int)Side.Left].Add(charaIndex);
                textureIDList.Add(TextureID.BattleTurnQueueFace1 + i);
                charaIndex++;
            }


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
        /// <summary>
        /// 生きているcharasのindexをすべて取得
        /// </summary>
        public List<int> AliveIndexes()
            => alive.Aggregate2(new List<int>(), (list, p, i) =>
            {
                if (p) list.Add(i);
                return list;
            });

        /// <summary>
        /// charas[index]がやられたときなどに戦場から取り除く
        /// </summary>
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
                while (turnQueue.Top().TurnTime > 0)
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

            bcgraphicsList.ForEach(e => e.Update());
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

            sb.DrawText(new Vector(400, 50), Resources.GetFont(FontID.CorpM), topTurn.CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            for (int i = 0; i < turnQueue.Count; i++)
                sb.DrawText(new Vector(510 + i * 110, 50), Resources.GetFont(FontID.CorpM), turnQueue[i].CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            sb.Draw(new Vector(450, 50), Resources.GetTexture(textureIDList[topTurn.CharaIndex]), DepthID.MenuTop);
            for (int i = 0; i < 5; i++)
                sb.Draw(new Vector(600 + i * TurnQueueTextureWidth, 50), Resources.GetTexture(textureIDList[turnQueue[i].CharaIndex]), DepthID.MenuTop);


            bcgraphicsList.ForEach(e => e.Draw(sb));
            for (int i = 3; i < charas.Count; i++)
            {
                sb.DrawText(new Vector(100 + (i - 3) * 180, 350), Resources.GetFont(FontID.CorpM), i.ToString() + ":", Color.White, DepthID.Message);
                //TODO:表示するステータスはchara[i].Statusから分離する
                sb.DrawText(new Vector(100 + (i - 3) * 180, 390), Resources.GetFont(FontID.CorpM), charas[i].Status.HP.ToString() + "/" + charas[i].Status.AScore.HPMAX.ToString(), Color.White, DepthID.Message, 0.75f);
            }


            MenuComponentList.ForEach(e => e.Draw(sb));
        }
    }
}
