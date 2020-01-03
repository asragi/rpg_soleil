using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 廊下その3
    /// </summary>
    class MagistolCol3 : MapBase
    {
        public override MusicID MapMusic => MusicID.MagicCity;
        private static readonly Vector[] ChairPos = new[]
        {
            new Vector(123, 1238),
            new Vector(1324, 535)
        };
        private const int ChairDiff = 80;
        public MagistolCol3(PersonParty party, Camera cam)
            : base(MapName.MagistolCol3, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol4_back, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(1745, 289), TextureID.Magistol4_wall, 542, om),
                new AdjustConstruct(ChairPos[0], TextureID.Magistol4_chair1, (int)ChairPos[0].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[1], TextureID.Magistol4_chair1, (int)ChairPos[1].Y + ChairDiff, om),
                new AdjustConstruct(new Vector(77, 1323), TextureID.Magistol4_chair2, 110, om),
                new AdjustConstruct(new Vector(284, 1368), TextureID.Magistol4_plant, 1512, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(2000, 1777);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(550, 1250),
                new CameraPoint(1500, 550),
                new CameraPoint(1004, 922),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            new MapChangeObject((new Vector(115, 1450), new Vector(205, 1503)), MapName.MagistolShop, new Vector(650, 500), Direction.LD, om, bm, party, cam);
        }
    }
}
