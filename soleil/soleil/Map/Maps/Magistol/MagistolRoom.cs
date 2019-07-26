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
    class MagistolRoom: MapBase
    {
        public MagistolRoom(PersonParty party, Camera cam)
           : base(MapName.MagistolRoom, party, cam)
        {
            MapConstructs = new MapConstruct[]
            {
                new MapConstruct(TextureID.Magistol1_back, MapDepth.Ground, om),
                new MapConstruct(new Vector(175, 131), TextureID.Magistol1_wall, MapDepth.Top, om), // 自販機
                new MapConstruct(new Vector(495, 408), TextureID.Magistol1_cloth, MapDepth.Top, om), // 右側の錆びた柵
            };

            // マップサイズの設定
            MapCameraManager.SetMapSize(960, 540);
            // CameraPointの設定
            CameraPoints = new CameraPoint[] {
                new CameraPoint(480, 270), // center
            };
            MapCameraManager.SetCameraPoint(CameraPoints);

            // Objects
        }
    }
}
