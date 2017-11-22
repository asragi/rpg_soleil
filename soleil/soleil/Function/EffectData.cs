using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCP
{

    static class EffectData
    {
        static List<EffectAnimationData> animationData;

        static EffectData()
        {
            animationData = new List<EffectAnimationData>()
            {
                new EffectAnimationData(EffectAnimationID.Blocking, new Vector(0, -60)),
                new EffectAnimationData(EffectAnimationID.Guard, new Vector(0, -60)),
                new EffectAnimationData(EffectAnimationID.Break, new Vector(-15, -60)),
                new EffectAnimationData(EffectAnimationID.HitL, new Vector(5,-60)),
                new EffectAnimationData(EffectAnimationID.HitH, new Vector(5,-60)),
                new EffectAnimationData(EffectAnimationID.StingHit,Vector.Zero,false,2),
                new EffectAnimationData(EffectAnimationID.Gauge,new Vector(0,-60)),
                new EffectAnimationData(EffectAnimationID.Special,new Vector(0,-60)),
                new EffectAnimationData(EffectAnimationID.Down,new Vector(0,-10)),
                new EffectAnimationData(EffectAnimationID.FireLimmited, new Vector(0,-60)),
                new EffectAnimationData(EffectAnimationID.AttackEffect, new Vector(0,-60)),
                new EffectAnimationData(EffectAnimationID.ThrowEffect, new Vector(0,-60)),
                new EffectAnimationData(EffectAnimationID.GuardEffect, new Vector(-10,-60)),
            };
        }

        public static EffectAnimationData GetEffect(EffectAnimationID id) => animationData[(int)id];
    }
}
