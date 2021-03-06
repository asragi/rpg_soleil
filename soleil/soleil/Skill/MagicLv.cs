﻿using System;
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

        public int this[MagicCategory cat]
        {
            get => dictMagicExp[cat];
        }

        public MagicLv(int[] exps, SkillHolder _skillHolder)
            : this(_skillHolder)
        {
            for (int i = 0; i < exps.Length; i++)
            {
                AddExp(exps[i], (MagicCategory)i);
            }
        }

        public MagicLv(SkillHolder _skillHolder)
        {
            skillHolder = _skillHolder;
            dictMagicExp = new Dictionary<MagicCategory, int>();
            for (int i = 0; i < (int)MagicCategory.size; ++i)
                dictMagicExp.Add((MagicCategory)i, 0);
        }
        /// <summary>
        /// MagicCategoryと対応するレベルを返します
        /// </summary>
        public int GetLv(MagicCategory category)
        {
            for (int i = 0; i < 9; i++)
            {
                // 次のLvまで(30*Lv)の経験値が必要 -> 累積が15*Lv*(Lv+1)以上になる．
                int thresh = 15 * i * (i + 1);
                if (dictMagicExp[category] <= thresh) return i;
            }
            return 9;
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
    }
}
