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
        public TestMap()
            : base(MapName.test)
        {

        }

        override public void Update()
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
