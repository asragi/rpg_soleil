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

        public void LearnSkill(SkillID id) => learnedFlags[(int)id] = true;

        public void ForgetSkill(SkillID id) => learnedFlags[(int)id] = false;
    }
}
