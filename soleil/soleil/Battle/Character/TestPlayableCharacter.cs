using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class TestPlayableCharacter : Character
    {
        public TestPlayableCharacter(int index) : base(index)
        {
            //てきとう
            var aScore = new AbilityScore(1800, 100, 100, 100, 100, 100);
            Status = new CharacterStatus(aScore, 10000, new List<Skill.SkillID> { Skill.SkillID.NormalAttack }, new List<Skill.SkillID> { });
            commandSelect = new DefaultPlayableCharacterCommandSelect(charaIndex, Status);
        }
    }
}
