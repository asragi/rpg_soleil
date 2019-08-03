using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// AbilityScoreのパラメータ毎の成長タイプ
    /// </summary>
    enum GrowthType
    {
        Precocious, // 早熟
        Normal, // 線形
        LateBloom, // 晩成
    }

    static class ExtendGrowthType
    {
        public static int GetParamsByLv(
            this GrowthType growth, int lv,
            double init, double last, int lvMax = 99)
        {
            switch (growth)
            {
                case GrowthType.Precocious: // 上に凸で(lv, val) = (1, init), (99, last)を通る2次式
                    return (int)(- ((last - init) / Math.Pow(lvMax - 1, 2)) * Math.Pow(lv - lvMax, 2) + last);
                case GrowthType.Normal: // Linear
                    return (int)(((last - init) / (lvMax - 1)) * (lv - 1) + init);
                case GrowthType.LateBloom: // 下に凸で(lv, val) = (1, init), (99, last)を通る2次式
                    return (int)(((last - init) / Math.Pow(lvMax - 1, 2)) * Math.Pow(lv - 1, 2) + init);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
