using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Somnia1 : MapBase
    {
        MapConstruct[] mapConstructs;
        CameraPoint[] cameraPoints;
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
                new CameraPoint(868,771),
                new CameraPoint(1381,1140),
                new CameraPoint(237,587),
                new CameraPoint(1312,282),
                new CameraPoint(620,440),
            };
            MapCameraManager.SetCameraPoint(cameraPoints);
        }
    }
}
