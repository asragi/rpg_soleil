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
            // 3分割された背景の位置設定用変数
            int width1 = Resources.GetTexture(TextureID.Flare1_1_1).Width;
            int width2 = width1 + Resources.GetTexture(TextureID.Flare1_1_2).Width;
            // マップ上の物体の召喚
            MapConstructs = new MapConstruct[]
            {
                // Grounds
                new FadeAnimationConstruct(Vector.Zero, new TextureID[] {TextureID.Flare1_1_1 }, 180, MapDepth.Ground, om),
                new FadeAnimationConstruct(new Vector(width1, 0) , new TextureID[] {TextureID.Flare1_1_2 }, 180, MapDepth.Ground, om),
                new FadeAnimationConstruct(new Vector(width2, 0) , new TextureID[] {TextureID.Flare1_1_3 }, 180, MapDepth.Ground, om),
                // Objects
                new MapConstruct(new Vector(3214, 2039), TextureID.Flare1_2,MapDepth.Top, om), // 右下の小さな壁
                new AdjustConstruct(new Vector(2811, 1832), TextureID.Flare1_3, 2000, om),
                new AdjustConstruct(new Vector(2598, 1615), TextureID.Flare1_4, 1950, om),
                new AdjustConstruct(new Vector(2065, 1590), TextureID.Flare1_5, 1880, om),
                new AdjustConstruct(new Vector(3559, 1503), TextureID.Flare1_6, 1850, om),
                new AdjustConstruct(new Vector(4155, 1455), TextureID.Flare1_7, 1790, om),
                new AdjustConstruct(new Vector(3239, 1366), TextureID.Flare1_8, 1620, om),
                new MapConstruct(new Vector(2725, 1220), TextureID.Flare1_9, MapDepth.Top, om),
                new AdjustConstruct(new Vector(1549, 1501), TextureID.Flare1_10, 1680, om),
                new AdjustConstruct(new Vector(1296, 1344), TextureID.Flare1_11, 1480, om),
            };
            // マップサイズの設定
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
