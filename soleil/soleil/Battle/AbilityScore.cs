namespace Soleil
{

    public struct AbilityScore
    {
        public int HPMAX;
        public int MPMAX;

        /// <summary>
        /// 物理攻撃の威力に関係
        /// [1,99]
        /// </summary>
        public int STR;

        /// <summary>
        /// 物理、魔法攻撃から受けるダメージの量に関係
        /// [1,99]
        /// </summary>
        public int VIT;

        /// <summary>
        /// 魔法攻撃の威力と魔法攻撃から受けるダメージ量に関係
        /// [1,99]
        /// </summary>
        public int MAG;

        /// <summary>
        /// 行動順、回避率、命中率に関係
        /// [1,99]
        /// </summary>
        public int SPD;
    }
}