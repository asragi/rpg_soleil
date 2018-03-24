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
        SelectableWindow testWindow;
        WindowManager wm;
        public TestObject(ObjectManager om, BoxManager bm, WindowManager wm)
            : base(om)
        {
            this.wm = wm;
            pos = new Vector(500, 300);
            exi = new CollideBox(this, Vector.Zero, new Vector(30, 30), CollideLayer.Character, bm);
        }

        public override void OnCollisionEnter()
        {
            testWindow = new SelectableWindow(pos, new Vector(300, 100), wm,
                "はい", "いいえ", "よさ");
            testWindow.active = false;
            base.OnCollisionEnter();
        }

        public override void OnCollisionExit()
        {
            if (testWindow != null) testWindow.Destroy();
            base.OnCollisionExit();
        }
    }
}
