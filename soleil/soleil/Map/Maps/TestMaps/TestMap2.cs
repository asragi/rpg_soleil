using System.Collections.Generic;

namespace Soleil
{
    class TestMap2 : Map
    {
        MapConstruct[] mapConstructs;
        WindowManager wm;
        public TestMap2(WindowManager w)
            : base(MapName.Somnia1)
        {
            wm = w;
            mapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia1_2,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia1_3,600,om),
                new AdjustConstruct(TextureID.Somnia1_4,850,om),
                new MapConstruct(TextureID.Somnia1_1,MapDepth.Top,om),
            };
            MapCameraManager.SetMapSize(1881, 1323);
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
