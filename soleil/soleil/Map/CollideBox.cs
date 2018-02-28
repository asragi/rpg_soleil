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
        static bool visible = true;

        Vector size;
        Vector localPos;
        Vector parentPos;
        MapObject parent;
        CollideLayer layer;

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(MapObject _parent, Vector _localPos, Vector _size, CollideLayer _layer)
        {
            parent = _parent;
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

        static public void Update()
        {
            CheckCollide();
        }

        static void CheckCollide()
        {

        }

        void CollideEnter()
        {
            parent.OnCollisionEnter();
        }

        void CollideStay()
        {
            parent.OnCollisionStay();
        }

        void CollideExit()
        {
            parent.OnCollisionExit();
        }

        Vector WorldPos() => parent.GetPosition() + localPos; // あまりよくない

        static public void Draw(Drawing d)
        {
            if (!visible) return;
            foreach (var item in boxList)
            {
                d.DrawBox(item.WorldPos(), item.size, Microsoft.Xna.Framework.Color.Red, DepthID.Debug);
            }
        }

    }
}
