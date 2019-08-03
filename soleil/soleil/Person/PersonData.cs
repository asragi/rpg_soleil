using Soleil.Item;
using Soleil.Misc;
using Soleil.Skill;

namespace Soleil
{
    /// <summary>
    /// キャラクター毎の能力値・初期装備・成長などの設定．
    /// </summary>
    struct PersonData
    {
        public GrowthType[] Growth;
        public AbilityScore InitScore;
        public AbilityScore LastScore;
        public ItemID[] InitEquip;
        public int[] InitMagicExp;
        public SkillID[] InitSkill;

        public PersonData(
            GrowthType[] growth, AbilityScore init, AbilityScore last,
            ItemID[] eq, int[] mgExp, SkillID[] sk)
        {
            Growth = growth;
            InitScore = init;
            LastScore = last;
            InitEquip = eq;
            InitMagicExp = mgExp;
            InitSkill = sk;
        }
    }
}
