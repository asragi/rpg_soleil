using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class MapCharacter:MapEventObject
    {
        protected Animation[] StandAnimation;
        public MapCharacter(Vector pos, Vector? boxSize, ObjectManager om, BoxManager bm)
            :base(pos,boxSize,CollideLayer.Player,om,bm)
        {
            // 8方向のアニメーション
            StandAnimation = new Animation[8];
        }
    }
}
