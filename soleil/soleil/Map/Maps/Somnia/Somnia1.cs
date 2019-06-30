using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Event;
using Soleil.Map.Maps.Somnia;

namespace Soleil.Map
{
    class Somnia1 : MapBase
    {
        // Objects
        Cigerman cigerman;

        MapChangeObject mcoLeft;
        MapChangeObject mcoTop;

        public Somnia1(PersonParty party, Camera cam)
            : base(MapName.Somnia1, party, cam)
        {
            MapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia1_2,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia1_5,470,om),
                new AdjustConstruct(TextureID.Somnia1_4,570,om),
                new AdjustConstruct(TextureID.Somnia1_3,570,om),
                new MapConstruct(TextureID.Somnia1_1,MapDepth.Top,om),
            };
            // マップサイズの設定
            MapCameraManager.SetMapSize(1260, 886);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(400, 300), // hotel entrance
                new CameraPoint(650, 555), // center
                new CameraPoint(1100, 840), // right down
                new CameraPoint(140, 455), // left
                new CameraPoint(880, 260),
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            cigerman = new Cigerman(new Vector(750, 600), om, bm);
            mcoLeft = new MapChangeObject(new Vector(0, 500), new Vector(30, 260), MapName.Somnia2, new Vector(906, 513), Direction.L, om, bm, Party, cam);
            mcoTop = new MapChangeObject(new Vector(900, 10), new Vector(300, 20), MapName.Somnia4, new Vector(896, 500), Direction.U, om, bm, Party, cam);
        }
    }
}
