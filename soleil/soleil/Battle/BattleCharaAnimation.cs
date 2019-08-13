using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Soleil
{
    enum BattleCharaMotionType : int
    {
        Stand,
        Chant,
        Magic,

        Size,
    }
    class BattleCharaAnimation
    {
        Animation[] animation;
        public Vector Pos;
        BattleCharaMotionType currentMotion;
        int timer = 0;
        public BattleCharaAnimation(Vector pos)
        {
            Pos = pos;
            currentMotion = BattleCharaMotionType.Stand;
            animation = new Animation[(int)BattleCharaMotionType.Size];
            animation[(int)BattleCharaMotionType.Stand] = new Animation(new AnimationData(AnimationID.BattleLuneStanding, true, 10));
            animation[(int)BattleCharaMotionType.Chant] = new Animation(new AnimationData(AnimationID.BattleLuneChant, false, 10));
            animation[(int)BattleCharaMotionType.Magic] = new Animation(new AnimationData(AnimationID.BattleLuneMagic, false, 10));
        }

        public void Update()
        {
            animation[(int)currentMotion].Move();

            if (timer > 0)
            {
                timer--;
                if (timer == 0)
                {
                    animation[(int)currentMotion].Reset();
                    currentMotion = BattleCharaMotionType.Stand;
                }
            }
        }

        public void SetMotion(BattleCharaMotionType motion, int time)
            => (currentMotion, timer) = (motion, time);

        public void Draw(Drawing d) =>
            animation[(int)currentMotion].Draw(d, Pos);
    }
}
