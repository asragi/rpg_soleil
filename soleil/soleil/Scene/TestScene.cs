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
        MessageWindow test;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            testMap = new TestMap();
            test = new MessageWindow(new Vector(100, 100), new Vector(300, 200), wm);
            test.SetMessage("これはテストメッセージです。");
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
