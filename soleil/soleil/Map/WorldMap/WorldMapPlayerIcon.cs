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
        public Vector Pos;
        public UIImage iconImg;

        public WorldMapPlayerIcon(WorldPoint playerpoint)
        {
            Point = playerpoint;
        }

        public void Update()
        {

        }

        public void Draw(Drawing d)
        {

        }
    }
}
