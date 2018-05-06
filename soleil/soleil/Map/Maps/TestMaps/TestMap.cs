using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil.Map
{
    class TestMap : MapBase
    {
        TestObject testO;
        TestObject2 test2;
        TestMapJump testJump;
        MapConstruct[] mapConstructs;
        CameraPoint[] cameraPoints;
        public TestMap()
            : base(MapName.Somnia2)
        {
            testO = new TestObject(om,bm);
            test2 = new TestObject2(om, bm);
            testJump = new TestMapJump(om, bm);
            mapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia6,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia4,600,om),
                new AdjustConstruct(TextureID.Somnia5,850,om),
                new MapConstruct(TextureID.Somnia1,MapDepth.Top,om),
                new MapConstruct(TextureID.Somnia2,MapDepth.Top,om),
            };
            MapCameraManager.SetMapSize(1505, 1058);
            cameraPoints = new CameraPoint[] {
                new CameraPoint(242,457),
                new CameraPoint(559,269),
                new CameraPoint(644,744),
                new CameraPoint(1068,779),
                new CameraPoint(235,965),
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
