using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    class TestMap : Map
    {
        TestObject testO;
        WindowManager wm;
        public TestMap(WindowManager w)
            : base(MapName.test)
        {
            wm = w;
            testO = new TestObject(om,bm,wm);
        }

        override public void Update()
        {
            base.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
        }
    }
}
