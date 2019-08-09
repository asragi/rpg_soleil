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

        Size,
    }
    class BattleCharaAnimation
    {
        Animation[] animation;
        public Vector Pos;
        BattleCharaMotionType currentMotion;
        public BattleCharaAnimation(Vector pos)
        {
            Pos = pos;
            currentMotion = BattleCharaMotionType.Stand;
            animation = new Animation[(int)BattleCharaMotionType.Size];
            animation[(int)BattleCharaMotionType.Stand] = new Animation(new AnimationData(AnimationID.BattleLuneStanding, true, 4));
        }

        public void Update() =>
            animation[(int)currentMotion].Move();

        public void Draw(Drawing d) =>
            animation[(int)currentMotion].Draw(d, Pos);
    }
}
