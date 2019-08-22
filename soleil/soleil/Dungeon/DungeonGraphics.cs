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
        private Image background;

        public DungeonGraphics()
        {
            background = new Image(TextureID.BattleTemporaryBackground, Vector.Zero, DepthID.BackGround, alpha: 1);
        }

        public void Update()
        {
            background.Update();
        }

        public void Draw(Drawing d)
        {
            background.Draw(d);
        }
    }
}
