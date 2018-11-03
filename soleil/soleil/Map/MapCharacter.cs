using Microsoft.Xna.Framework;
using Soleil.Event;
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
    abstract class MapCharacter : MapEventObject
    {
        readonly DepthID FrontDepth = DepthID.PlayerFront;
        readonly DepthID BackDepth = DepthID.PlayerBack;
        protected DepthID Depth;
        protected bool Symmetry; // アニメーションが左右対称かどうか
        public Direction Direction = Direction.D;
        protected MoveState MoveState;
        protected Animation NowAnimation;
        private Animation[] standAnimation;

        public MapCharacter(Vector pos, Vector? boxSize, ObjectManager om, BoxManager bm, bool _symmetry = true)
            :base(pos, boxSize, om, bm)
        {
            Symmetry = _symmetry;
            MoveState = MoveState.Stand;
            // n方向のアニメーション
            standAnimation = new Animation[9];
        }

        /// <summary>
        /// Stand状態のアニメーションを設定する.
        /// </summary>
        protected void SetStandAnimation(AnimationData[] data)
        {
            for (int i = 1; i < standAnimation.Length; i++)
            {
                standAnimation[i] = new Animation(data[i]);
            }
        }

        public override void Update()
        {
            base.Update();
            ChangeDepth();
            CheckMoveState();
            NowAnimation.Move();
        }

        protected virtual void CheckMoveState()
        {
            if (MoveState == MoveState.Stand)
            {
                NowAnimation = standAnimation[(int)Direction];
            }
        }

        protected virtual void ChangeDepth()
        {
            Depth = (Pos.Y > Player.Pos.Y) ? FrontDepth : BackDepth;
        }

        protected void ChangeDirection(Direction dir)
        {
            Direction = dir;
        }

        public override void Draw(Drawing sb)
        {
            NowAnimation?.DrawWithDepth(sb, Pos, Color.White, Depth);
            base.Draw(sb);
        }
    }
}
