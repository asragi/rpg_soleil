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
            // 移動イベントたち
            MapChangeObject mcoRight;
            MapChangeObject mcoLeft;
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Somnia4_1, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(715, 190), TextureID.Somnia4_2,335,om), // 自販機
                new AdjustConstruct(new Vector(821, 300), TextureID.Somnia4_3,385,om), // 右側の錆びた柵
                new AdjustConstruct(new Vector(74, 278), TextureID.Somnia4_4,520,om), // 手前の家
                new AdjustConstruct(TextureID.Somnia4_5, 445,om), // 手前の家
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            mcoLeft = new MapChangeObject(new Vector(103, 540), new Vector(206, 6), MapName.Somnia2, new Vector(307, 119), Direction.D, om, bm);
            mcoRight = new MapChangeObject(new Vector(858, 540), new Vector(206, 6), MapName.Somnia1, new Vector(880, 150), Direction.D, om, bm);
        }
    }
}
