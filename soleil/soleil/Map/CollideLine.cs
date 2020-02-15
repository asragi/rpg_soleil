using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 衝突判定用の線分．
    /// </summary>
    class CollideLine : CollideObject
    {
        public (Vector, Vector) Edges { get; private set; }

        public CollideLine(ICollideObject parent, (Vector, Vector) edge, CollideLayer layer, BoxManager bm)
            :base(parent, (edge.Item1 + edge.Item2) / 2, layer, bm)
        {
            Edges = edge;
        }

        public override void Draw(Drawing d) 
            => d.DrawLine(Edges.Item1, Edges.Item2, 1, Color.Red, DepthID.Player);
    }
}
