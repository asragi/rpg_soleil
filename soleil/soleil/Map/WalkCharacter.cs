using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    abstract class WalkCharacter : MapCharacter
    {
        protected Animation[] WalkAnimation;
        public WalkCharacter(Vector pos, Vector? boxSize,ObjectManager om, BoxManager bm, bool symmetry = true)
            : base(pos, boxSize, om, bm,symmetry)
        {
            WalkAnimation = symmetry ? new Animation[5] : new Animation[8];
        }
    }
}
