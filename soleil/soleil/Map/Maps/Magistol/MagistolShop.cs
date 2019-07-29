using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class MagistolShop: MapBase
    {
        private const int ChairDiff = 60;
        private static readonly Vector[] ChairPos = new[]
        {
                new Vector(137, 565),
                new Vector(738, 519),
                new Vector(422, 735)
        };
        public MagistolShop(PersonParty party, Camera cam)
         : base(MapName.MagistolShop, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol3_back, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(745, 318), TextureID.Magistol3_wall, 528, om),
                new AdjustConstruct(ChairPos[0], TextureID.Magistol3_chair, (int)ChairPos[0].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[1], TextureID.Magistol3_chair, (int)ChairPos[1].Y + ChairDiff - 10, om),
                new AdjustConstruct(ChairPos[2], TextureID.Magistol3_chair, (int)ChairPos[2].Y + ChairDiff, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(1000, 1000);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(500, 400),
                new CameraPoint(500, 600),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            // To Col3: Front of Principal room
            new MapChangeObject(new Vector(697, 465), new Vector(10, 60), MapName.MagistolCol3, new Vector(257, 1416), Direction.RU, om, bm, party, cam);
            // To Col1: West Side of corridor
            new MapChangeObject(new Vector(710, 730), new Vector(40, 40), MapName.MagistolCol1, new Vector(300, 417), Direction.RD, om, bm, party, cam);
        }
    }
}
