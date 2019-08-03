using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 衝突判定用Boxの管理・判定計算を行う．利用時はUpdate()とDraw()を呼ぶ．
    /// </summary>
    class BoxManager
    {
        protected readonly List<CollideObject> BoxList;
        int indexNum;
        bool visible = true; // for debug

        public BoxManager()
        {
            indexNum = 0;
            BoxList = new List<CollideObject>();
        }

        public void Add(CollideObject box)
        {
            BoxList.ForEach(b => b.ExtendBoolTable());
            box.ID = indexNum++;
            box.SetBoolTable(indexNum);
            BoxList.Add(box);
        }

        public virtual void Update()
        {
            CheckCollide();
            BoxList.ForEach(b => b.Update());
        }

        void CheckCollide()
        {
            for (int i = 0; i < BoxList.Count; i++)
            {
                for (int j = i + 1; j < BoxList.Count; j++)
                {
                    CalcCollide(i, j);
                }
            }
        }

        private void CalcCollide(int i, int j)
        {
            if(!BoxList[i].IsActive || !BoxList[j].IsActive)
            {
                // どちらかが非アクティブであれば衝突しない
                BoxList[j].Collide(BoxList[i], false);
                BoxList[i].Collide(BoxList[j], false);
                return;
            }

            bool col = false;
            if (BoxList[i] is CollideBox boxA && BoxList[j] is CollideBox boxB)
            {
                double xi = boxA.WorldPos.X,
                    yi = boxA.WorldPos.Y,
                    wi = boxA.Size.X,
                    hi = boxA.Size.Y;
                double xj = boxB.WorldPos.X,
                    yj = boxB.WorldPos.Y,
                    wj = boxB.Size.X,
                    hj = boxB.Size.Y;

                col = xi + wi / 2 > xj - wj / 2 && xi - wi / 2 < xj + wj / 2 &&
                    yi + hi / 2 > yj - hj / 2 && yi - hi / 2 < yj + hj / 2;
            }
            // 双方のboxに衝突相手の情報を渡す
            BoxList[j].Collide(BoxList[i],col);
            BoxList[i].Collide(BoxList[j],col);
        }

        public CollideObject GetBox(int id) => BoxList.Find(box => box.ID == id);

        public void Draw(Drawing d)
        {
            if (!visible) return;
            foreach (var item in BoxList)
            {
                item.Draw(d);
            }
        }
    }
}
