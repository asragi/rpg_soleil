using System.Collections.Generic;

namespace Soleil
{
    /// <summary>
    /// オブジェクト・マップの接触・衝突を管理する判定Boxクラス
    /// </summary>
    class CollideBox
    {
        public int ID { get; set; }
        public Vector Size { get; }
        Vector localPos;
        Vector parentPos;
        MapObject parent;
        CollideLayer layer;
        List<bool> preCollide, nowCollide;

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(MapObject _parent, Vector _localPos, Vector _size, CollideLayer _layer, BoxManager bm)
        {
            ID = -1;
            parent = _parent;
            parentPos = parent.GetPosition();
            localPos = _localPos;
            Size = _size;
            layer = _layer;
            preCollide = new List<bool>();
            nowCollide = new List<bool>();
            bm.Add(this);
        }

        public void SetLocalPos(Vector _pos) => localPos = _pos;
        public Vector GetLocalPos() => localPos;

        public void Update()
        {

            for (int i = 0; i < preCollide.Count; i++)
            {
                CollideStateCheck(i);
            }
        }

        private void CollideStateCheck(int i)
        {
            if (!preCollide[i] & nowCollide[i]) CollideEnter();
            if (preCollide[i] & nowCollide[i]) CollideStay();
            if (preCollide[i] & !nowCollide[i]) CollideExit();
            preCollide[i] = nowCollide[i];
        }

        /// <summary>
        /// 自身を含めたboxの数のListを与える
        /// </summary>
        public void SetBoolTable(int indexSize)
        {
            for (int i = 0; i < indexSize; i++)
            {
                ExtendBoolTable();
            }
        }

        /// <summary>
        /// boxがマップに追加されたときに衝突管理のList(bool)のサイズを1大きくする
        /// </summary>
        public void ExtendBoolTable()
        {   // 1Fくらい衝突ちゃんと取れてなくてもなんとかなるはず
            preCollide.Add(false);
            nowCollide.Add(false);
        }

        /// <summary>
        /// BoxManagerから呼び出す衝突のbool管理用関数
        /// </summary>
        public void Collide(CollideBox target, bool col)
        {
            nowCollide[target.ID] = col;
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
        public Vector WorldPos() => parent.GetPosition() + localPos; // あまりよくない
    }
}
