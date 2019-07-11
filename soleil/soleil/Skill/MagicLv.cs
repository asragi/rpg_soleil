using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    /// <summary>
    /// MagicCategoryと経験値を管理します
    /// </summary>
    class MagicLv
    {
        /// <summary>
        /// MagicCategoryと対応する経験値を保存します
        /// </summary>
        private Dictionary<MagicCategory, int> dictMagicExp;
        private SkillHolder skillHolder;
        /// <summary>
        /// MagicCategoryと対応するレベルを返します
        /// </summary>
        public int GetLv(MagicCategory category)
        {
            return dictMagicExp[category] / 10;
        }
        /// <summary>
        /// MagicCategoryが習得済みか
        /// </summary>
        public bool IsLearned(MagicCategory category) => GetLv(category) != 0;

        /// <summary>
        /// MagicCategoryに経験値valを加算
        /// </summary>
        public void AddExp(int val, MagicCategory category)
        {
            int lv = GetLv(category);
            dictMagicExp[category] = dictMagicExp[category] + val;
            int afterlv = GetLv(category);
            // スキル習得
            if (lv != afterlv)
            {
                for (int i = lv + 1; i <= afterlv; ++i)
                    if (SkillLearn.ExistLearningSkill(category, i))
                        skillHolder.LearnSkill(SkillLearn.GetLearnBy(category, i));
            }
        }

        public MagicLv(SkillHolder _skillHolder)
        {
            skillHolder = _skillHolder;
            dictMagicExp = new Dictionary<MagicCategory, int>();
            for (int i = 0; i < (int)MagicCategory.size; ++i)
                dictMagicExp.Add((MagicCategory)i, 0);
        }
    }
}
