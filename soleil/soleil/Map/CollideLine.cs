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

        public CollideLine(ICollideObject parent, Vector edge1, Vector edge2, CollideLayer layer, BoxManager bm)
            :base(parent, (edge1 + edge2) / 2, layer, bm)
        {
            Edges = ( edge1, edge2 );
        }

        public override void Draw(Drawing d) 
            => d.DrawLine(Edges.Item1, Edges.Item2, 1, Color.Red, DepthID.Player);
    }
}
