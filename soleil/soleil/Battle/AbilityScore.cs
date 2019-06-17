namespace Soleil
{

    public struct AbilityScore
    {
        public int HPMAX;
        public int MPMAX;

        private int hp;
        public int HP { get => hp; set => hp = MathEx.Clamp(value, HPMAX, 0); }
        private int mp;
        public int MP { get => mp; set => mp = MathEx.Clamp(value, MPMAX, 0); }

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

        public AbilityScore(int _HPMAX, int _MPMAX, int _STR, int _VIT, int _MAG, int _SPD)
        {
            HPMAX = _HPMAX;
            MPMAX = _MPMAX;
            (HP, MP) = (HPMAX, MPMAX);
            STR = _STR;
            VIT = _VIT;
            MAG = _MAG;
            SPD = _SPD;
        }
    }
}