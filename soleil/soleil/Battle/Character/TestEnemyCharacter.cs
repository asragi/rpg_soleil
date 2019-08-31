using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class TestEnemyCharacter : Character
    {
        public TestEnemyCharacter(int index, string enemyName) : base(index)
        {
            //てきとう
            var aScore = new AbilityScore(100, 30, 30, 30, 30, 30);
            Name = enemyName;
            Status = new CharacterStatus(aScore, 10000, new List<Skill.SkillID> { Skill.SkillID.NormalAttack }, new List<Skill.SkillID> { });
            commandSelect = new DefaultCharacterCommandSelect(charaIndex);
        }
    }
}
