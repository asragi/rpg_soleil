using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class TestPlayableCharacter : Character
    {
        public TestPlayableCharacter(int index) : base(index, CharacterType.TestEnemy)
        {
            //てきとう
            var aScore = new AbilityScore(1800, 100, 100, 100, 100, 100);
            Name = "Player" + (index + 1).ToString();
            Status = new CharacterStatus(aScore, 10000, new List<Skill.SkillID> { Skill.SkillID.NormalAttack }, new List<Skill.SkillID> { });
            commandSelect = new DefaultPlayableCharacterCommandSelect(CharacterIndex, Status);
        }
    }
}
