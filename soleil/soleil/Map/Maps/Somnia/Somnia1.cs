using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Map.Maps.Somnia;

namespace Soleil.Map
{
    class Somnia1 : MapBase
    {
        MapConstruct[] mapConstructs;
        CameraPoint[] cameraPoints;

        // Objects
        Cigerman cigerman;

        public Somnia1()
            : base(MapName.Somnia1)
        {
            mapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia1_2,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia1_3,600,om),
                new AdjustConstruct(TextureID.Somnia1_4,850,om),
                new MapConstruct(TextureID.Somnia1_1,MapDepth.Top,om),
                new AdjustConstruct(TextureID.Somnia1_5,600,om),
            };
            // マップサイズの設定
            MapCameraManager.SetMapSize(1260, 886);
            // CameraPointの設定
            cameraPoints = new CameraPoint[] {
                new CameraPoint(400, 300), // hotel entrance
                new CameraPoint(650, 555), // center
                new CameraPoint(1100, 840), // right down
                new CameraPoint(140, 455), // left
                new CameraPoint(880, 260),
            };
            MapCameraManager.SetCameraPoint(cameraPoints);

            // Objects
            cigerman = new Cigerman(new Vector(750, 600), om, bm);
        }
    }
}
