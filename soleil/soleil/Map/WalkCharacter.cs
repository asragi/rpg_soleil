﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    abstract class WalkCharacter : MapCharacter
    {
        private Animation[] walkAnimation;
        public WalkCharacter(string name, Vector pos, Vector? boxSize, ObjectManager om, BoxManager bm, bool symmetry = true)
            : base(name, pos, boxSize, om, bm, symmetry)
        {
            walkAnimation = new Animation[9];
        }

        protected void SetWalkAnimation(AnimationData[] data)
        {
            for (int i = 1; i < walkAnimation.Length; i++)
            {
                walkAnimation[i] = new Animation(data[i]);
            }
        }

        /// <summary>
        /// WIP
        /// </summary>
        public virtual void Move(Direction dir)
        {
            if (dir == Direction.N)
            {
                MoveState = MoveState.Stand;
                return;
            }
            Direction = dir;
        }

        /// <summary>
        /// 基底クラスのUpdateから呼び出される。MoveStateに応じてアニメーションを変更する。
        /// </summary>
        protected override void CheckMoveState()
        {
            if (MoveState == MoveState.Walk)
            {
                NowAnimation = walkAnimation[(int)Direction];
            }
            base.CheckMoveState();
        }
    }
}