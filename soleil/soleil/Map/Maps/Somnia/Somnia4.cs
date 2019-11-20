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
        public override MusicID MapMusic => MusicID.ShadeCity;
        public Somnia4(PersonParty party, Camera cam)
            : base(MapName.Somnia4, party, cam)
        {
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
            new AccessaryGirl(new Vector(650, 330), party, om, bm);
            new MapChangeObject((new Vector(0, 530), new Vector(206, 530)), MapName.Somnia2, new Vector(307, 119), Direction.D, om, bm, Party, cam);
            new MapChangeObject((new Vector(752, 530), new Vector(960, 530)), MapName.Somnia1, new Vector(880, 150), Direction.D, om, bm, Party, cam);
        }
    }
}
