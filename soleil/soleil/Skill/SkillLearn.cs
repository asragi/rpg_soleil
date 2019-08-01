using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    /// <summary>
    /// スキルを習得するレベルを保管します
    /// </summary>
    static class SkillLearn
    {
        /// <summary>
        /// MagicCategoryのレベルで習得するスキルを保管します
        /// </summary>
        public static readonly Dictionary<MagicCategory, Dictionary<int, SkillID>> skillLearn;
        static SkillLearn()
        {
            skillLearn = new Dictionary<MagicCategory, Dictionary<int, SkillID>>();
            for (int i = 0; i < (int)MagicCategory.size; ++i)
            {
                skillLearn.Add((MagicCategory)i, new Dictionary<int, SkillID>());
                // skillLeanにデータ追加
            }
            // skillLearnへのデータ追加に使用します
            void Set(int lv, SkillID skill)
            {
                var data = (MagicData)SkillDataBase.Get(skill);
                skillLearn[data.Category].Add(lv, skill);
            }
        }
        /// <summary>
        /// MagicCategoryのレベルで覚えるスキルidを返します
        /// </summary>
        public static SkillID GetLearnBy(MagicCategory category, int lv) => skillLearn[category][lv];

        /// <summary>
        /// MagicCategoryのレベルで覚えるスキルの有無を返します
        /// </summary>
        public static bool ExistLearningSkill(MagicCategory category, int lv) => skillLearn[category].ContainsKey(lv);
    }
}
