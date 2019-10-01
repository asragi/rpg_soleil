using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// CharaSeelctWindowで選択した結果を持つ
    /// CommandSelectに渡す
    /// </summary>
    struct SelectItems
    {
        public CommandEnum Command;
        public Skill.SkillID AName;
        public Range.AttackRange ARange;
    }
}
