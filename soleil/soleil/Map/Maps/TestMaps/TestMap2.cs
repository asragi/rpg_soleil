using System.Collections.Generic;

namespace Soleil.Map
{
    class TestMap2 : MapBase
    {
        MapConstruct[] mapConstructs;
        CameraPoint[] cameraPoints;
        TestMapJump2 test;

        public TestMap2()
            : base(MapName.Somnia1)
        {
            test = new TestMapJump2(om, bm);
            mapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia1_2,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia1_3,600,om),
                new AdjustConstruct(TextureID.Somnia1_4,850,om),
                new MapConstruct(TextureID.Somnia1_1,MapDepth.Top,om),
            };
            // マップサイズの設定
            MapCameraManager.SetMapSize(1881, 1323);
            // CameraPointの設定
            cameraPoints = new CameraPoint[] {
                new CameraPoint(868,771),
                new CameraPoint(1381,1140),
                new CameraPoint(237,587),
                new CameraPoint(1312,282),
                new CameraPoint(620,440),
            };
            MapCameraManager.SetCameraPoint(cameraPoints);
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
