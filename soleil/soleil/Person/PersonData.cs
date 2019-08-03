using Soleil.Item;
using Soleil.Misc;
using Soleil.Skill;

namespace Soleil
{
    struct PersonData
    {
        public AbilityScore InitScore;
        public ItemID[] InitEquip;
        public int[] InitMagicExp;
        public SkillID[] InitSkill;

        public PersonData(AbilityScore score, ItemID[] eq, int[] mgExp, SkillID[] sk)
        {
            InitScore = score;
            InitEquip = eq;
            InitMagicExp = mgExp;
            InitSkill = sk;
        }
    }
}
