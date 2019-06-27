using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// WorldMap上のプレイヤーの位置を示すアイコン
    /// </summary>
    class WorldMapPlayerIcon
    {
        public WorldPoint Point { get; set; }
        public Image iconImg;
        public Vector Pos
        {
            get => iconImg.Pos;
            set => iconImg.Pos = value;
        }

        public WorldMapPlayerIcon(WorldPoint playerpoint)
        {
            Point = playerpoint;
            iconImg = new Image(TextureID.BackBar, Point.Pos,
                Vector.Zero, DepthID.Player,
                true, false, 1);
        }

        public void Update()
        {
            iconImg.Update();
        }

        public void Draw(Drawing d)
        {
            iconImg.Draw(d);
        }
    }
}
