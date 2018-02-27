using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum CollideLayer
    {
        Player,
        Wall,
        Character,

        /// <summary>
        /// プレイヤーと重なる接触型イベントフラグ
        /// </summary>
        RoadEvent,
    }

    /// <summary>
    /// オブジェクト・マップの接触・衝突を管理するクラス
    /// </summary>
    class CollideBox
    {
        static List<CollideBox> boxList;
        static MapData mapData;
        static PlayerObject player;

        Vector size;
        Vector localPos;
        Vector parentPos;
        CollideLayer layer;

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(MapObject parent, Vector _localPos, Vector _size, CollideLayer _layer)
        {
            parentPos = parent.GetPosition();
            localPos = _localPos;
            size = _size;
            layer = _layer;
            boxList = boxList ?? new List<CollideBox>();
            boxList.Add(this);
        }

        /// <summary>
        /// Mapのコンストラクタで呼び出す。
        /// </summary>
        public static void Init(PlayerObject _player, MapData _data)
        {
            mapData = _data;
            player = _player;
        }

        public void CheckCollide()
        {

        }
    }
}
