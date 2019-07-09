using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 衝突判定用Boxの管理・判定計算を行う
    /// </summary>
    class BoxManager
    {
        protected readonly List<CollideBox> BoxList;
        int indexNum;
        bool visible = true; // for debug

        public BoxManager()
        {
            indexNum = 0;
            BoxList = new List<CollideBox>();
        }

        public void Add(CollideBox box)
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
            double xi = BoxList[i].WorldPos().X, 
                yi = BoxList[i].WorldPos().Y, 
                wi = BoxList[i].Size.X, 
                hi = BoxList[i].Size.Y;
            double xj = BoxList[j].WorldPos().X, 
                yj = BoxList[j].WorldPos().Y,
                wj = BoxList[j].Size.X,
                hj = BoxList[j].Size.Y;

            bool col = xi + wi/2 > xj - wj/2 && xi - wi/2 < xj + wj/2 &&
                yi + hi / 2 > yj - hj / 2 && yi - hi / 2 < yj + hj / 2;

            // 双方のboxに衝突相手の情報を渡す
            BoxList[j].Collide(BoxList[i],col);
            BoxList[i].Collide(BoxList[j],col);
        }

        public CollideBox GetBox(int id) => BoxList.Find(box => box.ID == id);

        public void Draw(Drawing d)
        {
            if (!visible) return;
            foreach (var item in BoxList)
            {
                d.DrawBox(item.WorldPos(), item.Size, Color.Red, DepthID.Player);
            }
        }
    }
}
