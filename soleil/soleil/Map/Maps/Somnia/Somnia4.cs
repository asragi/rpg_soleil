using Soleil.Event;
using Soleil.Map.Maps.Somnia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Somnia4 : MapBase
    {
        // Object
        AccessaryGirl accessaryGirl;
        // 移動イベントたち
        MapChangeObject mcoRight;
        MapChangeObject mcoLeft;
        // Event
        EventSequence firstEvent;

        public Somnia4(PersonParty party)
            : base(MapName.Somnia4, party)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Somnia4_1, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(715, 190), TextureID.Somnia4_2,335,om), // 自販機
                new AdjustConstruct(new Vector(821, 300), TextureID.Somnia4_3,385,om), // 右側の錆びた柵
                new AdjustConstruct(new Vector(74, 278), TextureID.Somnia4_4,520,om), // 手前の家
                new AdjustConstruct(TextureID.Somnia4_5, 445,om), // 手前の家
            };

            // Event
            firstEvent = new EventSequence(om.GetPlayer());
            firstEvent.SetEventSet(
                CreateConversationEvent.Create("somnia", "battle", ConversationSystem)
            );
            EventSequences = new EventSequence[] { firstEvent };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            accessaryGirl = new AccessaryGirl(new Vector(650, 330), om, bm);
            mcoLeft = new MapChangeObject(new Vector(103, 540), new Vector(206, 6), MapName.Somnia2, new Vector(307, 119), Direction.D, om, bm, Party);
            mcoRight = new MapChangeObject(new Vector(858, 540), new Vector(206, 6), MapName.Somnia1, new Vector(880, 150), Direction.D, om, bm, Party);
        }

        protected override void Start()
        {
            base.Start();
            firstEvent.StartEvent();
        }
    }
}
