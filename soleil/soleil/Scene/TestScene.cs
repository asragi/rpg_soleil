using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{
    class TestScene : Scene
    {
        TestMap testMap;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            testMap = new TestMap();
            new Window(new Vector(100, 100), new Vector(300, 200), wm);
        }

        override public void Update()
        {
            testMap.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            testMap.Draw(sb);
            base.Draw(sb);
        }
    }
}
