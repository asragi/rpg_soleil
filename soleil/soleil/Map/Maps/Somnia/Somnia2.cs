using Soleil.Map.Maps.Somnia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class Somnia2 : MapBase
    {
        MapChangeObject mcoRight;
        MapChangeObject mcoTop;
        public Somnia2(PersonParty party, Camera cam)
            : base(MapName.Somnia2, party, cam)
        {
            MapConstructs = new MapConstruct[]{
                new MapConstruct(TextureID.Somnia2_1,MapDepth.Ground,om),
                new AdjustConstruct(TextureID.Somnia2_2,436,om), // R House
                new AdjustConstruct(TextureID.Somnia2_3,613,om), // Left House
                new AdjustConstruct(TextureID.Somnia2_4,593,om), // Drumcan
                new MapConstruct(TextureID.Somnia2_5,MapDepth.Top,om), // temae
                new MapConstruct(TextureID.Somnia2_6,MapDepth.Top,om), // Shadow
            };
            // マップサイズの設定
            MapCameraManager.SetMapSize(1054, 741);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(494, 516), // center
                new CameraPoint(400, 291), // top
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            mcoTop = new MapChangeObject(new Vector(200, 10), new Vector(200, 20), MapName.Somnia4, new Vector(100, 500), Direction.U, om, bm, Party, cam);
            mcoRight = new MapChangeObject(new Vector(970, 553), new Vector(30, 260), MapName.Somnia1, new Vector(100, 500), Direction.R, om, bm, Party, cam);
        }
    }
}
