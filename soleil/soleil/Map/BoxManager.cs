using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 衝突判定用Boxの管理・判定計算を行う
    /// </summary>
    class BoxManager
    {
        List<CollideBox> boxList;
        int indexNum;
        bool visible = false; // for debug
        MapData mapData;
        PlayerObject player;

        public BoxManager(MapData data, PlayerObject pl)
        {
            indexNum = 0;
            boxList = new List<CollideBox>();
            mapData = data;
            player = pl;
        }

        public void Add(CollideBox box)
        {
            boxList.ForEach(b => b.ExtendBoolTable());
            box.ID = indexNum++;
            box.SetBoolTable(indexNum);
            boxList.Add(box);
        }

        public void Update()
        {
            CheckCollide();
            CheckWallCollide();
            boxList.ForEach(b => b.Update());
        }

        void CheckWallCollide()
        {
            for (int i = 0; i < boxList.Count; i++)
            {
                double xi = boxList[i].WorldPos().X,
                    yi = boxList[i].WorldPos().Y,
                    wi = boxList[i].Size.X,
                    hi = boxList[i].Size.Y;
                bool col = false;
                for (int j = 0; j < wi; j++)
                {
                    for (int k = 0; k < hi; k++)
                    {
                        col = col || CalcWallCollide(xi, yi, wi, hi, j, k); // 一つでもtrueならtrue
                    }
                }
                boxList[i].SetWallCollide(col);
            }
        }

        private bool CalcWallCollide(double xi, double yi, double wi, double hi, int j, int k)
        {
            int checkX = (int)(xi - wi / 2 + j);
            int checkY = (int)(yi - hi / 2 + k);
            // マップ外は壁ではないとする.
            if (checkX >= mapData.GetFlagLengthX() || checkX < 0) return false;
            if (checkY >= mapData.GetFlagLengthY() || checkY < 0) return false;
            return mapData.GetFlagData(checkX, checkY);
        }

        void CheckCollide()
        {
            for (int i = 0; i < boxList.Count; i++)
            {
                for (int j = i + 1; j < boxList.Count; j++)
                {
                    CalcCollide(i, j);
                }
            }
        }

        private void CalcCollide(int i, int j)
        {
            double xi = boxList[i].WorldPos().X, 
                yi = boxList[i].WorldPos().Y, 
                wi = boxList[i].Size.X, 
                hi = boxList[i].Size.Y;
            double xj = boxList[j].WorldPos().X, 
                yj = boxList[j].WorldPos().Y,
                wj = boxList[j].Size.X,
                hj = boxList[j].Size.Y;

            bool col = xi + wi/2 > xj - wj/2 && xi - wi/2 < xj + wj/2 &&
                yi + hi / 2 > yj - hj / 2 && yi - hi / 2 < yj + hj / 2;

            /*
            Vector[] boxj = new Vector[]
            {
                // box[j]の4頂点の位置を取る
                new Vector(boxList[j].WorldPos().X-boxList[j].Size.X/2,boxList[j].WorldPos().Y-boxList[j].Size.Y/2),
                new Vector(boxList[j].WorldPos().X+boxList[j].Size.X/2,boxList[j].WorldPos().Y+boxList[j].Size.Y/2),
                new Vector(boxList[j].WorldPos().X-boxList[j].Size.X/2,boxList[j].WorldPos().Y+boxList[j].Size.Y/2),
                new Vector(boxList[j].WorldPos().X+boxList[j].Size.X/2,boxList[j].WorldPos().Y-boxList[j].Size.Y/2),
            };
            for (int c = 0; c < 4; c++)
            {
                col = ((boxList[i].WorldPos().X - boxList[i].Size.X / 2 < boxj[c].X) && (boxj[c].X < boxList[i].WorldPos().X + boxList[i].Size.X / 2))
                    && ((boxList[i].WorldPos().Y - boxList[i].Size.Y / 2 < boxj[c].Y) && (boxj[c].Y < boxList[i].WorldPos().Y + boxList[i].Size.Y / 2));
                if (col) break; // 1頂点でもbox[i]に含まれていることがわかれば計算を終了する
            }*/


            // 双方のboxに衝突相手の情報を渡す
            boxList[j].Collide(boxList[i],col);
            boxList[i].Collide(boxList[j],col);
        }

        public void Draw(Drawing d)
        {
            if (!visible) return;
            foreach (var item in boxList)
            {
                d.DrawBox(item.WorldPos(), item.Size, Color.Red, DepthID.Player);
            }
        }
    }
}
