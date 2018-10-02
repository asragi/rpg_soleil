﻿using System.Collections.Generic;

namespace Soleil.Map
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
        public CollideLayer Layer;
        List<bool> preCollide, nowCollide;
        bool wallCollide; // 壁に重なっているか
        public bool IsActive;
        BoxManager boxManager;

        /// <param name="_localPos">相対的な矩形中心位置</param>
        public CollideBox(MapObject _parent, Vector _localPos, Vector _size, CollideLayer _layer, BoxManager bm)
        {
            ID = -1;
            parent = _parent;
            parentPos = parent.GetPosition();
            localPos = _localPos;
            Size = _size;
            Layer = _layer;
            preCollide = new List<bool>();
            nowCollide = new List<bool>();
            IsActive = true;
            bm.Add(this);
            boxManager = bm;
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
            if (!preCollide[i] & nowCollide[i]) CollideEnter(i);
            if (preCollide[i] & nowCollide[i]) CollideStay();
            if (preCollide[i] & !nowCollide[i]) CollideExit();
            preCollide[i] = nowCollide[i];
        }

        public void SetWallCollide(bool _col)
        {
            wallCollide = _col;
        }

        public bool GetWallCollide() => wallCollide;

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

        void CollideEnter(int id)
        {
            parent.OnCollisionEnter(boxManager.GetBox(id));
        }

        void CollideStay()
        {
            parent.OnCollisionStay();
        }

        void CollideExit()
        {
            parent.OnCollisionExit();
        }

        /// <summary>
        /// 他のキャラクターと衝突しているかどうか．
        /// </summary>
        public bool GetCollideCharacter()
        {
            for (int i = 0; i < nowCollide.Count; i++)
            {
                // キャラクターレイヤーでないならスキップ
                if (boxManager.GetBox(i).Layer != CollideLayer.Character) continue;
                // 判定対象Boxのparentが自身のparentと同一であればスキップ
                if (boxManager.GetBox(i).parent == parent) continue;
                if (nowCollide[i]) return true;
            }
            return false;
        }

        public Vector WorldPos() => parent.GetPosition() + localPos; // あまりよくない
    }
}
