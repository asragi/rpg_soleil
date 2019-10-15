using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 開始位置．ルーネの部屋
    /// </summary>
    class MagistolRoom : MapBase
    {
        public MagistolRoom(PersonParty party, Camera cam)
           : base(MapName.MagistolRoom, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol1_back, MapDepth.Ground, om),
                new AdjustConstruct(new Vector(314, 72), TextureID.Magistol1_wall, 362, om),
                new MapConstruct(new Vector(175, 131), TextureID.Magistol1_dark, MapDepth.Top, om),
                new MapConstruct(new Vector(495, 408), TextureID.Magistol1_cloth, MapDepth.Top, om),
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
            new MapChangeObject((new Vector(280, 264), new Vector(376, 308)), MapName.MagistolCol1, new Vector(1232, 1222), Direction.U, om, bm, party, cam);
        }
    }
}
