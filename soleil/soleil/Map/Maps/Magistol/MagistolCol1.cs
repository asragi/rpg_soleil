using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 廊下その1
    /// </summary>
    class MagistolCol1: MapBase
    {
        private static readonly Vector[] ChairPos = new[]
        {
            new Vector(196, 486),
            new Vector(586, 825),
            new Vector(677, 893),
            new Vector(1007, 1181)
        };
        private const int ChairDiff = 80;
        public MagistolCol1(PersonParty party, Camera cam)
            :base(MapName.MagistolCol1, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol2_back, MapDepth.Ground, om),
                new AdjustConstruct(ChairPos[0], TextureID.Magistol2_chair, (int)ChairPos[0].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[1], TextureID.Magistol2_chair, (int)ChairPos[1].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[2], TextureID.Magistol2_chair, (int)ChairPos[2].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[3], TextureID.Magistol2_chair, (int)ChairPos[3].Y + ChairDiff, om),
                new AdjustConstruct(new Vector(1341, 1070), TextureID.Magistol2_plant, 1200, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(1500, 1500);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(418, 400),
                new CameraPoint(644, 650),
                new CameraPoint(1004, 1072),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
        }
    }
}
