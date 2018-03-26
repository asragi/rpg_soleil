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
        MapEventManager eventManager;
        public TestObject(ObjectManager om, BoxManager bm)
            : base(om)
        {
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
            eventManager = MapEventManager.GetInstance();
        }

        public override void OnCollisionEnter()
        {
            eventManager.CreateMessageWindow(pos,new Vector(200,100),0);
            eventManager.CreateMessageWindow(pos + new Vector(100,100), new Vector(200, 100), 0);
            base.OnCollisionEnter();
        }

        public override void OnCollisionExit()
        {
            eventManager.DestroyWindow(0);
            base.OnCollisionExit();
        }
    }
}
