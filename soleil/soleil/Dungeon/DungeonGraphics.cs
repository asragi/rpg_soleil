using Soleil.Images;
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
        private Image background;
        private TopInfo topInfo;

        public DungeonGraphics()
        {
            background = new Image(TextureID.BattleTemporaryBackground, Vector.Zero, DepthID.BackGround, alpha: 1);
            topInfo = new TopInfo(TopInfoPos, Vector.Zero, "マギストル地下", 3);
            topInfo.Call();
        }

        public void Update()
        {
            background.Update();
            topInfo.Update();
        }

        public void Draw(Drawing d)
        {
            background.Draw(d);
            topInfo.Draw(d);
        }

        /// <summary>
        /// 画面左上に表示するダンジョン名など．
        /// </summary>
        class TopInfo: MenuComponent
        {
            private static readonly Vector BackImgDiff = new Vector(-100, 4);
            private const int BackImgLength = 400;
            private static FontID Font = FontID.CorpM;
            private BackBarImage backImg;
            private TextImage textImg;
            
            /// <param name="pos">文字の開始位置</param>
            public TopInfo(Vector pos, Vector posDiff, string name, int floor)
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
    }
}
