using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// ぶつかり判定図形の基底クラス．
    /// </summary>
    abstract class CollideObject
    {
        public int ID { get; set; } = -1;
        public Vector LocalPos { get; set; }
        public bool IsActive { get; set; } = true;
        ICollideObject parent;
        public CollideLayer Layer;
        List<bool> preCollide, nowCollide;
        protected BoxManager BoxManager;
        public bool WallCollide { get; set; } // 壁に重なっているか

        public CollideObject(ICollideObject _parent, Vector _localPos, CollideLayer layer, BoxManager bm)
        {
            parent = _parent;
            LocalPos = _localPos;
            Layer = layer;
            preCollide = new List<bool>();
            nowCollide = new List<bool>();
            bm.Add(this);
            BoxManager = bm;
        }

        public Vector WorldPos => parent.GetPosition() + LocalPos;

        /// <summary>
        /// boxがマップに追加されたときに衝突管理のList(bool)のサイズを1大きくする
        /// </summary>
        public void ExtendBoolTable()
        {   // 1Fくらい衝突ちゃんと取れてなくてもなんとかなるはず
            preCollide.Add(false);
            nowCollide.Add(false);
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
        /// BoxManagerから呼び出す衝突のbool管理用関数
        /// </summary>
        public void Collide(CollideObject target, bool col)
        {
            nowCollide[target.ID] = col;
        }

        public void Update()
        {
            for (int i = 0; i < preCollide.Count; i++)
            {
                CollideStateCheck(i);
            }

            void CollideStateCheck(int i)
            {
                if (!preCollide[i] & nowCollide[i]) CollideEnter(i);
                if (preCollide[i] & nowCollide[i]) CollideStay(i);
                if (preCollide[i] & !nowCollide[i]) CollideExit(i);
                preCollide[i] = nowCollide[i];
            }

            void CollideEnter(int id) => parent.OnCollisionEnter(BoxManager.GetBox(id));
            void CollideStay(int id) => parent.OnCollisionStay(BoxManager.GetBox(id));
            void CollideExit(int id) => parent.OnCollisionExit(BoxManager.GetBox(id));
        }

        /// <summary>
        /// 他のキャラクターと衝突しているかどうか．
        /// </summary>
        public bool GetCollideCharacter()
        {
            for (int i = 0; i < nowCollide.Count; i++)
            {
                // キャラクターレイヤーでないならスキップ
                if (BoxManager.GetBox(i).Layer != CollideLayer.Character) continue;
                // 判定対象Boxのparentが自身のparentと同一であればスキップ
                if (BoxManager.GetBox(i).parent == parent) continue;
                if (nowCollide[i]) return true;
            }
            return false;
        }

        public abstract void Draw(Drawing d);
    }
}
