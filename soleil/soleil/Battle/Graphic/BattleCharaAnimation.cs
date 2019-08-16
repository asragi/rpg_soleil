using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Soleil.Battle
{
    enum BattleCharaAnimationType
    {
        Stand,
        Chant,
        Magic,

        Size,
    }
    class BattleCharaAnimation
    {
        public Vector Pos;
        Animation[] animation;
        BattleCharaAnimationType currentAnimationType;
        BattleCharaMotion bcMotion;
        BattleCharaMotionType currentMotionType;
        public BattleCharaAnimation(Vector pos)
        {
            Pos = pos;
            currentAnimationType = BattleCharaAnimationType.Stand;
            animation = new Animation[(int)BattleCharaAnimationType.Size];
            animation[(int)BattleCharaAnimationType.Stand] = new Animation(new AnimationData(AnimationID.BattleLuneStanding, true, 10));
            animation[(int)BattleCharaAnimationType.Chant] = new Animation(new AnimationData(AnimationID.BattleLuneChant, true, 10));
            animation[(int)BattleCharaAnimationType.Magic] = new Animation(new AnimationData(AnimationID.BattleLuneMagic, false, 10));

            SetMotion(BattleCharaMotionType.Stand);
        }


        public void Update()
        {
            var (retAnimType, retMotionType) = bcMotion.Update(this);
            if (retAnimType != currentAnimationType)
            {
                animation[(int)currentAnimationType].Reset();
                currentAnimationType = retAnimType;
            }
            if (retMotionType != currentMotionType)
            {
                currentMotionType = retMotionType;
                SetMotion(currentMotionType);
            }
            animation[(int)currentAnimationType].Move();
        }

        public void SetMotion(BattleCharaMotionType motion)
        {
            switch (motion)
            {
                case BattleCharaMotionType.Stand:
                    bcMotion = new BattleCharaMotion(motion);
                    break;
                case BattleCharaMotionType.Chant:
                    bcMotion = new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Chant, 60);
                    break;
                case BattleCharaMotionType.Magic:
                    bcMotion = new BattleCharaMotionSeq(motion, new List<BattleCharaMotion>{
                        new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Chant, 60),
                        new BattleCharaMotionWithTime(motion, BattleCharaAnimationType.Magic, 75)
                        });
                    break;
            }
        }

        public void Draw(Drawing d) =>
            animation[(int)currentAnimationType].Draw(d, Pos);
    }
}
