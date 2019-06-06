using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    enum WorldPointKey
    {
        Flare = 1,
        Somnia = 2,
        Magistol = 3,
        Parel = 5,
        Shimaki = 6,
        Earthband = 7,
        AisenBerz = 8,
        size,
    }

    class WorldPoint
    {
        public readonly WorldPointKey ID; // csvの頂点のIDと一致させる．
        public readonly Vector Pos; // ワールドマップ上の座標．
        public Dictionary<WorldPointKey, int> edges;

        public bool IsPlayerIn;
        public bool Selected;

        public WorldPoint(WorldPointKey id, Vector position)
        {
            ID = id;
            Pos = position;
        }

        public void SetEdge(WorldPointKey key, int cost) => edges.Add(key, cost);

        public void Draw(Drawing d)
        {
            var color = IsPlayerIn ? Color.Crimson : Color.AliceBlue;
            d.DrawBox(Pos, new Vector(30, 30), color, DepthID.HitBox);
        }
    }
}
