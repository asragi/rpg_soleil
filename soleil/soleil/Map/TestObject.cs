using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestObject :MapObject
    {
        CollideBox exi;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
        }
    }
}
