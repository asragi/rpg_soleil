using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    /// <summary>
    /// キャラクターが持つスキルを管理するクラス
    /// </summary>
    class SkillHolder
    {
        bool[] learnedFlags;
        public SkillHolder()
        {
            learnedFlags = new bool[(int)SkillID.size];
        }

        public bool HasSkill(SkillID id) => learnedFlags[(int)id];

        public bool HasCategory(MagicCategory c)
        {
            for (int i = 0; i < learnedFlags.Length; i++)
            {
                if (!learnedFlags[i]) continue;
                var data = SkillDataBase.Get((SkillID)i);
                if (data is MagicData md)
                {
                    if (md.Category == c) return true;
                }
            }
            return false;
        }

        public void LearnSkill(SkillID id) => learnedFlags[(int)id] = true;

        public void ForgetSkill(SkillID id) => learnedFlags[(int)id] = false;
    }
}
