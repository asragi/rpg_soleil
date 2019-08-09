using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// Map上での衝突判定を行うクラス
    /// </summary>
    class MapBoxManager : BoxManager
    {
        MapData mapData;
        public MapBoxManager(MapData data) : base()
        {
            mapData = data;
        }

        public override void Update()
        {
            CheckWallCollide();
            base.Update();
        }

        void CheckWallCollide()
        {
            for (int i = 0; i < BoxList.Count; i++)
            {
                double xi = BoxList[i].WorldPos().X,
                    yi = BoxList[i].WorldPos().Y,
                    wi = BoxList[i].Size.X,
                    hi = BoxList[i].Size.Y;
                bool col = false;
                for (int j = 0; j < wi; j++)
                {
                    for (int k = 0; k < hi; k++)
                    {
                        col = col || CalcWallCollide(xi, yi, wi, hi, j, k); // 一つでもtrueならtrue
                    }
                }
                BoxList[i].SetWallCollide(col);
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
    }
}
