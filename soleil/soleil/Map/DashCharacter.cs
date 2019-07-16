using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// Map上のオブジェクトのうち、Dashを実装するもの。
    /// </summary>
    abstract class DashCharacter : WalkCharacter
    {
        private Animation[] dashAnimation;
        public DashCharacter(string name, Vector pos,Vector? boxSize,ObjectManager om, BoxManager bm, bool symmetry = true)
            :base(name, pos,boxSize,om,bm, symmetry)
        {
            dashAnimation = new Animation[9];
        }

        /// <summary>
        /// Stand状態のアニメーションを設定する.
        /// </summary>
        protected void SetDashAnimation(AnimationData[] data)
        {
            for (int i = 1; i < dashAnimation.Length; i++)
            {
                dashAnimation[i] = new Animation(data[i]);
            }
        }

        /// <summary>
        /// 基底クラスのUpdateから呼び出される。MoveStateに応じてアニメーションを変更する。
        /// </summary>
        protected override void CheckMoveState()
        {
            if (MoveState == MoveState.Dash)
            {
                NowAnimation = dashAnimation[(int)Direction];
            }
            base.CheckMoveState();
        }
    }
}
