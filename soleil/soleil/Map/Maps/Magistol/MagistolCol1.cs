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
    class MagistolCol1 : MapBase
    {
        public override MusicID MapMusic => MusicID.MagicCity;
        private static readonly Vector[] ChairPos = new[]
        {
            new Vector(196, 486),
            new Vector(586, 825),
            new Vector(677, 893),
            new Vector(1007, 1181)
        };
        private const int ChairDiff = 80;

        public MagistolCol1(PersonParty party, Camera cam)
            : base(MapName.MagistolCol1, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol2_back, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(822, 406), TextureID.Magistol2_wall, 710, om),
                new AdjustConstruct(ChairPos[0], TextureID.Magistol2_chair, (int)ChairPos[0].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[1], TextureID.Magistol2_chair, (int)ChairPos[1].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[2], TextureID.Magistol2_chair, (int)ChairPos[2].Y + ChairDiff, om),
                new AdjustConstruct(ChairPos[3], TextureID.Magistol2_chair, (int)ChairPos[3].Y + ChairDiff, om),
                new AdjustConstruct(new Vector(440, 250), TextureID.Magistol2_shelf, 435, om),
                new AdjustConstruct(new Vector(1341, 1070), TextureID.Magistol2_plant, 1200, om),
                new AdjustConstruct(new Vector(87, 96), TextureID.Magistol2_stair, 414, om),
                new AdjustConstruct(new Vector(97, 308), TextureID.Magistol2_plant, 438, om),
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
            new MapChangeObject((new Vector(280, 280), new Vector(144, 357)), MapName.MagistolShop, new Vector(665, 690), Direction.LU, om, bm, party, cam);
            new MapChangeObject((new Vector(1271, 1305), new Vector(1319, 1278)), MapName.MagistolRoom, new Vector(338, 400), Direction.RD, om, bm, party, cam);
            new WorldmapObject("tmp", new Vector(853, 400), new Vector(30, 30), WorldMap.WorldPointKey.Magistol, party, om, bm);

            // debug
            EventSequences = new Event.EventSequence[1];
            EventSequences[0] = new Event.EventSequence(om.GetPlayer());
            EventSequences[0].SetEventSet(
                new Event.MessageWindowEvent(new Vector(550, 500), 0, "サニーが仲間になった！"),
                new Event.ChangeInputFocusEvent(InputFocus.Player),
                new Event.CharacterActivateEvent(Party, Misc.CharaName.Sunny, true)
                );
        }

        protected override void Start()
        {
            base.Start();
            if (!Party.Get(Misc.CharaName.Sunny).InParty)
            {
                EventSequences[0].StartEvent();
            }
        }
    }
}
