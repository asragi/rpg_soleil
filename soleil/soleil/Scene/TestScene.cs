using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class TestScene : Scene
    {
        public TestScene(SceneManager sm)
            : base(sm)
        {

        }

        override public void Update()
        {
            Console.WriteLine("おけぴー");
            base.Update();
        }
    }
}
