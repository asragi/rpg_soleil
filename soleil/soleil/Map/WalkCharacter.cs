using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    abstract class WalkCharacter : MapCharacter
    {
        public WalkCharacter(Vector pos, Vector? boxSize,ObjectManager om, BoxManager bm)
            : base(pos, boxSize, om, bm)
        {

        }
    }
}
