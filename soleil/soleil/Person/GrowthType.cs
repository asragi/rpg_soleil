using System;

namespace Soleil
{
    /// <summary>
    /// AbilityScoreのパラメータ毎の成長タイプ
    /// </summary>
    enum GrowthType
    {
        Pre, // 早熟(Precocious)
        Normal, // 線形
        Late, // 晩成(LateBloom)
    }

    /// <summary>
    /// Lvからパラメータを計算するクラス．
    /// </summary>
    class GrowthParams
    {
        public static AbilityScore GetParamsByLv(
            GrowthType[] growth, AbilityScore init, AbilityScore last, int lv)
        {
            int hp = GetParam(growth[0], lv, init.HPMAX, last.HPMAX);
            int mp = GetParam(growth[1], lv, init.MPMAX, last.MPMAX);
            int str = GetParam(growth[2], lv, init.STR, last.STR);
            int vit = GetParam(growth[3], lv, init.VIT, last.VIT);
            int mag = GetParam(growth[4], lv, init.MAG, last.MAG);
            int spd = GetParam(growth[5], lv, init.SPD, last.SPD);
            return new AbilityScore(hp, mp, str, vit, mag, spd);
        }

        /// <summary>
        /// Lvをもとに一つのパラメータを計算する．
        /// </summary>
        private static int GetParam(
            GrowthType growth, int lv,
            double init, double last, int lvMax = 99)
        {
            switch (growth)
            {
                case GrowthType.Pre: // 上に凸で(lv, val) = (1, init), (99, last)を通る2次式
                    return (int)(- ((last - init) / Math.Pow(lvMax - 1, 2)) * Math.Pow(lv - lvMax, 2) + last);
                case GrowthType.Normal: // Linear
                    return (int)(((last - init) / (lvMax - 1)) * (lv - 1) + init);
                case GrowthType.Late: // 下に凸で(lv, val) = (1, init), (99, last)を通る2次式
                    return (int)(((last - init) / Math.Pow(lvMax - 1, 2)) * Math.Pow(lv - 1, 2) + init);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
