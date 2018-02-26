﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

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

        override public void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
