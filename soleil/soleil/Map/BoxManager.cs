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
        bool visible = false; // for debug

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
            if (!BoxList[i].IsActive || !BoxList[j].IsActive)
            {
                // どちらかが非アクティブであれば衝突しない
                BoxList[j].Collide(BoxList[i], false);
                BoxList[i].Collide(BoxList[j], false);
                return;
            }

            bool col = false;
            // 矩形と矩形のぶつかり
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
            // 矩形vs線分
            if (BoxList[i] is CollideLine _line1 && BoxList[j] is CollideBox _box1)
                col = LineCollide(_line1, _box1);
            if (BoxList[i] is CollideBox _box2 && BoxList[j] is CollideLine _line2)
                col = LineCollide(_line2, _box2);
            // note: 線分を動かす予定はないので線分vs線分は計算しない（常にfalse）

            // 双方のboxに衝突相手の情報を渡す
            BoxList[j].Collide(BoxList[i],col);
            BoxList[i].Collide(BoxList[j],col);

            bool LineCollide(CollideLine line, CollideBox box)
            {
                // BoxのWorldPosは矩形中心．LineのWorldPosは線分中点．
                // 線分の中点がBoxの内側にあれば衝突しているといえる．
                // 線分が完全に箱の内側に入り込む場合，後述の計算で衝突が取れない．
                var target = line.WorldPos;
                if (target.X <= box.WorldPos.X + box.Size.X / 2
                    && target.X >= box.WorldPos.X - box.Size.X / 2
                    && target.Y <= box.WorldPos.Y + box.Size.Y / 2
                    && target.Y >= box.WorldPos.Y - box.Size.Y / 2)
                    return true;

                // 矩形の4辺どれかと線分が交差していればtrue
                Vector wPos = box.WorldPos, half = box.Size / 2;
                var points = new[]
                {
                    wPos + new Vector(half.X, half.Y),
                    wPos + new Vector(half.X, -half.Y),
                    wPos + new Vector(-half.X, -half.Y),
                    wPos + new Vector(-half.X, half.Y)
                };
                var sides = new (Vector, Vector)[4];
                for (int k = 0; k < sides.Length; k++)
                {
                    sides[k] = (points[k], points[(k + 1) % 4]);
                }

                for (int k = 0; k < sides.Length; k++)
                {
                    if (Line2LineCheck(line.Edges, sides[k]))
                        return true;
                }
                return false;

                // 線分と線分の交差判定
                bool Line2LineCheck((Vector, Vector) a, (Vector, Vector) b)
                {
                    Vector a1 = a.Item1, a2 = a.Item2, b1 = b.Item1, b2 = b.Item2;
                    // 直線aと線分bが交差
                    Func<double, double, double> funcA // 直線aの式
                        = (x, y) => (a1.X - a2.X) * (y - a1.Y) + (a1.Y - a2.Y) * (a1.X - x);
                    // -- b2の両端点について直線aの式に代入した値が同符号 -> 交差していない
                    if (funcA(b1.X, b1.Y) * funcA(b2.X, b2.Y) > 0) return false;
                    // 直線bと線分aが交差
                    Func<double, double, double> funcB // 直線bの式
                        = (x, y) => (b1.X - b2.X) * (y - b1.Y) + (b1.Y - b2.Y) * (b1.X - x);
                    if (funcB(a1.X, a1.Y) * funcB(a2.X, a2.Y) > 0) return false;
                    return true;
                }
            }
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
