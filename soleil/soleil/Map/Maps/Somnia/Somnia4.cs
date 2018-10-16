using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Somnia4 : MapBase
    {
        public Somnia4()
            : base(MapName.Somnia4)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Somnia4_1, MapDepth.Ground, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);
        }
    }
}
