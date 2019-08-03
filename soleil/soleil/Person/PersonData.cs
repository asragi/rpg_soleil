using Soleil.Item;
using Soleil.Misc;
using Soleil.Skill;

namespace Soleil
{
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
