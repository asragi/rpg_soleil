using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Soleil.Map
{
    /// <summary>
    /// オブジェクト・マップの接触・衝突を管理する判定Boxクラス
    /// </summary>
    class CollideBox: CollideObject
    {
        public Vector Size { get; }

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(ICollideObject _parent, Vector _localPos, Vector _size, CollideLayer _layer, BoxManager bm)
            : base(_parent, _localPos, _layer, bm)
        {
            Size = _size;
        }

        public override void Draw(Drawing d)
        {
            d.DrawBox(WorldPos, Size, Color.Red, DepthID.Player);
        }
    }
}
