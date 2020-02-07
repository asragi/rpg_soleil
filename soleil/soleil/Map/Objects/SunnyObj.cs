using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.Objects
{
    /// <summary>
    /// マップ上に表示されるサニーのオブジェクト
    /// </summary>
    class SunnyObj : WalkCharacter
    {
        public SunnyObj(Vector pos, ObjectManager om, BoxManager bm)
            : base("Sunny", pos, null, om, bm)
        {
            SetAnimation();
        }

        private void SetAnimation()
        {
            var posDiff = new Vector(0, -50);
            var standAnims = new AnimationData[9];
            var sPeriod = 8;
            standAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneStandR, posDiff, true, sPeriod);
            standAnims[(int)Direction.RD] = new AnimationData(AnimationID.LuneStandDR, posDiff, true, sPeriod);
            standAnims[(int)Direction.D] = new AnimationData(AnimationID.LuneStandD, posDiff, true, sPeriod);
            standAnims[(int)Direction.LD] = new AnimationData(AnimationID.LuneStandDL, posDiff, true, sPeriod);
            standAnims[(int)Direction.L] = new AnimationData(AnimationID.LuneStandL, posDiff, true, sPeriod);
            standAnims[(int)Direction.LU] = new AnimationData(AnimationID.LuneStandUL, posDiff, true, sPeriod);
            standAnims[(int)Direction.U] = new AnimationData(AnimationID.LuneStandU, posDiff, true, sPeriod);
            standAnims[(int)Direction.RU] = new AnimationData(AnimationID.LuneStandUR, posDiff, true, sPeriod);
            SetStandAnimation(standAnims);

            var walkAnims = new AnimationData[9];
            var wPeriod = 8;
            walkAnims[(int)Direction.R] = new AnimationData(AnimationID.LuneWalkR, posDiff, true, wPeriod);
            walkAnims[(int)Direction.RD] = new AnimationData(AnimationID.LuneWalkDR, posDiff, true, wPeriod);
            walkAnims[(int)Direction.D] = new AnimationData(AnimationID.LuneWalkD, posDiff, true, wPeriod);
            walkAnims[(int)Direction.LD] = new AnimationData(AnimationID.LuneWalkDL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.L] = new AnimationData(AnimationID.LuneWalkL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.LU] = new AnimationData(AnimationID.LuneWalkUL, posDiff, true, wPeriod);
            walkAnims[(int)Direction.U] = new AnimationData(AnimationID.LuneWalkU, posDiff, true, wPeriod);
            walkAnims[(int)Direction.RU] = new AnimationData(AnimationID.LuneWalkUR, posDiff, true, wPeriod);
            SetWalkAnimation(walkAnims);
        }
    }
}
