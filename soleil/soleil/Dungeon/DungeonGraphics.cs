﻿using Soleil.Images;
using Soleil.Map;
using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeonでのグラフィック関連を管理するクラス．
    /// </summary>
    class DungeonGraphics
    {
        private readonly static Vector TopInfoPos
            = new Vector(30, 30);
        private readonly static Vector SearchInfoPos
            = new Vector(0, 40);
        private Image background;
        private FloorInfo topInfo;
        private SearchInfo searchInfo;
        private MapIndicator indicator;

        public DungeonGraphics(DungeonState state)
        {
            background = new Image(TextureID.BattleTemporaryBackground, Vector.Zero, DepthID.BackGround, alpha: 1);
            topInfo = new FloorInfo(TopInfoPos, Vector.Zero, "マギストル地下", 1);
            topInfo.Call();
            searchInfo = new SearchInfo(TopInfoPos + SearchInfoPos, Vector.Zero, state);
            searchInfo.Call();
            indicator = new MapIndicator();
        }

        public void NextFloor(DungeonState state)
        {
            topInfo.FloorNum = state.FloorNum;
        }

        public void Update()
        {
            background.Update();
            topInfo.Update();
            searchInfo.Update();
            indicator.Update();
        }

        public void Draw(Drawing d)
        {
            background.Draw(d);
            topInfo.Draw(d);
            searchInfo.Draw(d);
            indicator.Draw(d);
        }

        /// <summary>
        /// 画面左上に表示するダンジョン名など．
        /// </summary>
        class TopInfo: EasingComponent
        {
            private static readonly Vector BackImgDiff = new Vector(-100, 4);
            private const int BackImgLength = 400;
            public readonly static FontID Font = FontID.CorpM;
            private BackBarImage backImg;
            private TextImage textImg;
            
            /// <param name="pos">文字の開始位置</param>
            public TopInfo(Vector pos, Vector posDiff, string name)
            {
                textImg = new TextImage(Font, pos, posDiff, DepthID.Message);
                textImg.Text = name;
                textImg.ActivateOutline(1);
                backImg = new BackBarImage(
                    pos + BackImgDiff, posDiff, BackImgLength,
                    false, DepthID.MessageBack);
                AddComponents(textImg, backImg);
            }
        }

        /// <summary>
        /// TopInfo + Floorの階層情報
        /// </summary>
        class FloorInfo: EasingComponent
        {
            private const int FontDiff = 10;
            private readonly Vector FloorDiff = new Vector(200, FontDiff);
            private readonly Vector FloorNumDiff = new Vector(70, 0);
            private readonly string FloorText = "Floor";
            TopInfo baseInfo;
            TextImage floorImg;
            RightAlignText floorNumImg;

            public FloorInfo(Vector pos, Vector posDiff, string name, int floor)
            {
                var font = FontID.CorpMini;
                baseInfo = new TopInfo(pos, posDiff, name);
                floorImg = new TextImage(font, pos + FloorDiff, posDiff, DepthID.Message);
                floorImg.Text = FloorText;
                floorNumImg = new RightAlignText(
                    font, pos + FloorNumDiff + FloorDiff, posDiff,
                    DepthID.Message);
                FloorNum = floor;

                floorImg.ActivateOutline(1);
                floorNumImg.ActivateOutline(1);

                AddComponents(baseInfo, floorNumImg, floorImg);
            }

            public int FloorNum { set => floorNumImg.Text = value.ToString(); }
        }

        /// <summary>
        /// 左上に表示する探索完了情報．
        /// </summary>
        class SearchInfo: EasingComponent
        {
            private readonly Vector FloorDiff = new Vector(70, 0);
            private readonly string SearchText = "探索:";
            private readonly string UnknownText = "？？？？？？？？？";

            private readonly DungeonState dState;
            TopInfo baseInfo;
            TextImage searchResultImg;

            public SearchInfo(Vector pos, Vector posDiff, DungeonState state)
            {
                dState = state;
                baseInfo = new TopInfo(pos, posDiff, SearchText);
                searchResultImg = new TextImage(TopInfo.Font, pos + FloorDiff, DepthID.Message);
                searchResultImg.Text = UnknownText;
                searchResultImg.ActivateOutline(1);
                AddComponents(baseInfo, searchResultImg);
            }

            public override void Update()
            {
                base.Update();
                ChangeFoundState();
            }

            private void ChangeFoundState()
            {
                var targetEvent = DungeonDatabase.Get(dState.Name).GetEvent(dState.FloorNum);
                string message = targetEvent.DisplayName;
                string target = targetEvent.Achieved ? message : UnknownText;
                searchResultImg.Text = target;
            }
        }
    }
}
