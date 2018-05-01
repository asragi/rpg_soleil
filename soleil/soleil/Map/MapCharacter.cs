using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    enum MoveState { Stand,Walk,Dash }

    /// <summary>
    /// Map上の存在判定のあるオブジェクトのうち、移動しないもの。
    /// </summary>
    abstract class MapCharacter:MapEventObject
    {
        protected bool Symmetry; // アニメーションが左右対称かどうか
        protected ObjectDir Direction;
        protected MoveState MoveState;
        protected Animation NowAnimation;
        protected Animation[] StandAnimation;
        public MapCharacter(Vector pos, Vector? boxSize, ObjectManager om, BoxManager bm, bool _symmetry = true)
            :base(pos,boxSize,CollideLayer.Player,om,bm)
        {
            Symmetry = _symmetry;
            MoveState = MoveState.Stand;
            // n方向のアニメーション
            StandAnimation = Symmetry ? new Animation[5] : new Animation[8];
        }

        protected void ChangeDirection(ObjectDir dir)
        {
            Direction = dir;
        }

        void ChangeAnimStand()
        {
            NowAnimation = StandAnimation[(int)Direction];
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
        }

    }
}
