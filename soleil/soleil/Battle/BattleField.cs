using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Soleil.Battle
{
    /// <summary>
    /// 戦闘での陣営
    /// 左が敵サイド、右が自陣サイドを想定
    /// </summary>
    enum Side
    {
        Left,
        Right,
        Size,
    }

    /// <summary>
    /// 戦闘全体を管理する singleton
    /// </summary>
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

        public List<Effect> Effects;

        bool onEnd;

        /// <summary>
        /// priorityでソートされたConditionedEffect
        /// ターンを超えて起こる効果を持つ
        /// </summary>
        public SortedSet<ConditionedEffect> CEffects;
        public BattleField()
        {
        }

        public void InitBattle(PersonParty party, List<EnemyCharacter> enemies)
        {
            onEnd = false;
            MenuComponentList = new List<Menu.MenuComponent>();

            var partylist = party.GetActiveMembers();

            charaIndex = 0;
            charas = new List<Character>();
            sides = new List<Side>();
            indexes = new List<int>[(int)Side.Size] { new List<int>(), new List<int>() };
            Effects = new List<Effect>();
            textureIDList = new List<TextureID>();
            var faceDict = new Dictionary<Misc.CharaName, TextureID>
            {
                {Misc.CharaName.Lune, TextureID.BattleTurnQueueFaceLune },
                {Misc.CharaName.Sunny, TextureID.BattleTurnQueueFaceSun },
                {Misc.CharaName.Tella, TextureID.BattleTurnQueueFace4 },
            };
            for (int i = 0; i < partylist.Length; ++i)
            {
                textureIDList.Add(faceDict[partylist[i].Name]);
            }
            for (int i = 0; i < partylist.Length; i++)
            {
                var chara = new PlayableCharacter(charaIndex, partylist[i].Score, partylist[i], new Vector(750 - (partylist.Length - i - 1) * 200, 450), new Vector(600 + i * 50, 200 + i * 100));
                charas.Add(chara);

                sides.Add(Side.Right);
                indexes[(int)Side.Right].Add(charaIndex);
                charaIndex++;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                charas.Add(enemies[i].Generate(charaIndex, new Vector(100 + i * 200, 350), new Vector(300 - i * 50, 200 + i * 100)));
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

            Enumerable.Range(0, charaIndex).ForEach2(p => CEffects.Add(new ConditionedEffectOnce(
                (act) => GetCharacter(p).Status.Dead,
                (act, ocrs) =>
                {
                    RemoveCharacter(p);
                    ocrs.Add(new Occurence(GetCharacter(p).Name + "はやられた"));
                    return ocrs;
                },
                5000)));
            CEffects.Add(new ConditionedEffectOnce(
                //どちらかのSideが全滅したか判定
                (act) => Enumerable.Range(0, 2).Select(i => indexes[i].Count == 0).Any(p => p),
                (act, ocrs) =>
                {
                    ocrs.Add(new OccurenceBattleEnd());
                    return ocrs;
                },
                4900
                ));

        }

        public void AddTurn(Turn turn) => turnQueue.Push(turn);
        public void AddTurn(List<Turn> turn) => turnQueue.PushAll(turn);


        /// <summary>
        /// Shallow CopyされたConditionedEffectのSortedSet
        /// (取得した先での破壊的変更がCEffectsにも反映される)
        /// </summary>
        public SortedSet<ConditionedEffect> GetCopiedCEffects()
            => new SortedSet<ConditionedEffect>(CEffects);
        public void AddCEffect(ConditionedEffect cEffect) => CEffects.Add(cEffect);

        public Character GetCharacter(int index) => charas[index];


        public Side GetSide(int index) => sides[index];
        /// <summary>
        /// 相手のSideを取得する
        /// </summary>
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
        public Side OppositeSide(int index) => OppositeSide(sides[index]);
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
        /// <summary>
        /// battleQueがあればそれを実行する
        /// 無くてかつ最後に取り出したBattleEventが実行終了していればTurnQueueを取り出し、
        /// Turnを実行する
        /// </summary>
        public void Update()
        {
            if (onEnd)
            {
                OnEnd();
                return;
            }
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
                    ocrs.ForEach(e =>
                    {
                        switch (e)
                        {
                            case OccurenceBattleEnd ocr:
                                battleQue.Enqueue(new BattleMessage(ocr.Message, 0));
                                battleQue.Enqueue(new BattleEnd(180, ocr.DidWin));
                                break;
                            default:
                                battleQue.Enqueue(new BattleEffect(e));
                                break;
                        }
                    });
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
                case BattleEffect be:
                    message = be.Occur.Message;
                    if (executed)
                        be.Act();
                    executed = delayCount <= 1;
                    break;
                case BattleEnd be:
                    //とりあえず勝ったとき TODO:敗北
                    executed = false;
                    if (be.DidWin)
                        charas.ForEach(e => e.Win());
                    if (delayCount < 0 && KeyInput.GetKeyPush(Key.A))
                    {
                        //sceneの切り替え
                        executed = true;
                        delayCount = 0;
                        // とりあえずの実装 by ragi
                        onEnd = true;
                        endWait = EndWaitMax;
                        var transition = Transition.GetInstance();
                        transition.SetMode(TransitionMode.FadeOut);
                    }
                    if (delayCount == 1)
                        delayCount = -1;
                    break;
            }

            charas.ForEach(e => e.Update());
            Effects.ForEach(e => e.Move());
            Effects.RemoveAll(e => e.Disable);
        }

        public void EnqueueTurn(Turn turn) => turnQueue.Push(turn);


        /// <summary>
        /// turnQueueにPushされてないかつ一番最初にターンが回ってくるTurnをPushする
        /// </summary>
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

        SceneManager sceneManager;
        public void SetSceneManager(SceneManager sm) => sceneManager = sm;
        private const int EndWaitMax = 60;
        private int endWait;
        private void OnEnd()
        {
            endWait--;
            if (endWait < 0)
            {
                sceneManager.KillNowScene();
            }
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
            sb.DrawText(new Vector(400, 50), Resources.GetFont(FontID.CorpM), topTurn.CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
            for (int i = 0; i < turnQueue.Count; i++)
                sb.DrawText(new Vector(510 + i * 110, 50), Resources.GetFont(FontID.CorpM), turnQueue[i].CharaIndex.ToString() + "のターン", Color.White, DepthID.Message);
                */
            if (topTurn != null)
                sb.Draw(new Vector(450, 50), Resources.GetTexture(textureIDList[topTurn.CharaIndex]), DepthID.MenuTop);
            for (int i = 0; i < 5; i++)
                sb.Draw(new Vector(600 + i * TurnQueueTextureWidth, 50), Resources.GetTexture(textureIDList[turnQueue[i].CharaIndex]), DepthID.MenuTop);


            Effects.ForEach2(e => e.Draw(sb));
            charas.ForEach(e => e.Draw(sb));
            /*for (int i = 3; i < charas.Count; i++)
            {
                sb.DrawText(new Vector(100 + (i - 3) * 180, 350), Resources.GetFont(FontID.CorpM), i.ToString() + ":", Color.White, DepthID.Message);
                //TODO:表示するステータスはchara[i].Statusから分離する
                sb.DrawText(new Vector(100 + (i - 3) * 180, 390), Resources.GetFont(FontID.CorpM), charas[i].Status.HP.ToString() + "/" + charas[i].Status.AScore.HPMAX.ToString(), Color.White, DepthID.Message, 0.75f);
            }*/


            MenuComponentList.ForEach(e => e.Draw(sb));
        }
    }
}
