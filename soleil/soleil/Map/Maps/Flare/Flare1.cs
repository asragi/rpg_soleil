using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Flare1 : MapBase
    {
        public Flare1()
            : base(MapName.Flare1)
        {
            int width1 = Resources.GetTexture(TextureID.Flare1_1_1).Width;
            int width2 = width1 + Resources.GetTexture(TextureID.Flare1_1_2).Width;
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Flare1_1_1, MapDepth.Ground,om),
                new MapConstruct(new Vector(width1,0), TextureID.Flare1_1_2, MapDepth.Ground,om),
                new MapConstruct(new Vector(width2,0), TextureID.Flare1_1_3, MapDepth.Ground,om),
            };
            MapCameraManager.SetMapSize(8090, 2895);

            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(1000, 1320),
                new CameraPoint(1130, 1540), // hotel entrance
                new CameraPoint(1916, 1775), // center
                new CameraPoint(2858, 2000), // right down
                new CameraPoint(3351, 1778), // left
                new CameraPoint(2768, 1400),
                new CameraPoint(3300, 2200),
                new CameraPoint(3800, 2100),
                new CameraPoint(4370, 1800),
                new CameraPoint(5320, 1500),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);
        }

    }
}
