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
    class MapCharacter:MapEventObject
    {
        protected ObjectDir Direction;
        protected MoveState MoveState;
        protected Animation NowAnimation;
        protected Animation[] StandAnimation;
        public MapCharacter(Vector pos, Vector? boxSize, ObjectManager om, BoxManager bm)
            :base(pos,boxSize,CollideLayer.Player,om,bm)
        {
            MoveState = MoveState.Stand;
            // 8方向のアニメーション
            StandAnimation = new Animation[8];
        }

        protected void ChangeDirection(ObjectDir dir)
        {
            Direction = dir;
        }

        void ChangeAnimStand(ObjectDir dir)
        {
            NowAnimation = StandAnimation[(int)dir];
        }
    }
}
