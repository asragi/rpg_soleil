using Soleil.Map.Maps.Somnia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Somnia2 : MapBase
    {
        public Somnia2()
            : base(MapName.Somnia2)
        {
            MapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia2_1,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia2_2,500,om),
                new AdjustConstruct(TextureID.Somnia2_3,470,om),
                new AdjustConstruct(TextureID.Somnia2_4,570,om),
                new MapConstruct(TextureID.Somnia2_5,MapDepth.Top,om),
                new MapConstruct(TextureID.Somnia2_6,MapDepth.Top,om),
            };
            // マップサイズの設定
            MapCameraManager.SetMapSize(1054, 741);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(400, 300), // hotel entrance
                new CameraPoint(650, 555), // center
                new CameraPoint(1100, 840), // right down
                new CameraPoint(140, 455), // left
                new CameraPoint(880, 260),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
        }
    }
}
