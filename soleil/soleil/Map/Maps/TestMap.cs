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
        MapConstruct[] mapConstructs;
        List<CollideBox> hoge;
        WindowManager wm;
        public TestMap(WindowManager w)
            : base(MapName.test)
        {
            wm = w;
            testO = new TestObject(om,bm,wm);
            
            mapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia6,MapDepth.Ground,om),
                /*
                new MapConstruct(TextureID.Somnia4,MapDepth.Adjust,om),
                new MapConstruct(TextureID.Somnia5,MapDepth.Adjust,om),
                */
                new AdjustConstruct(TextureID.Somnia4,300,om),
                new AdjustConstruct(TextureID.Somnia5,500,om),
                new MapConstruct(TextureID.Somnia1,MapDepth.Top,om),
                new MapConstruct(TextureID.Somnia2,MapDepth.Top,om),
            };

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
