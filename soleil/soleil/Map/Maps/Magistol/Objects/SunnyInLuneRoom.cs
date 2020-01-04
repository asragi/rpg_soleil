using Soleil.Map.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.Maps.Magistol
{
    class SunnyInLuneRoom : SunnyObj
    {
        public SunnyInLuneRoom(Vector pos, ObjectManager om, BoxManager bm)
            : base (pos, om, bm)
        {

        }

        public override void OnCollisionEnter(CollideObject collide)
        {
            base.OnCollisionEnter(collide);
            if (collide.Layer != CollideLayer.PlayerHit) return;
            FaceToPlayer();
        }
    }
}
