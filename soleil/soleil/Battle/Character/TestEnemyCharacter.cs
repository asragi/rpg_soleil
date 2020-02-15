using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class TestEnemyCharacter : Character
    {
        public TestEnemyCharacter(int index, string enemyName, Vector statusPos, Vector charaPos)
            : base(index, CharacterType.TestEnemy)
        {
            //てきとう
            var aScore = new AbilityScore(100, 10, 10, 10, 10, 10);
            Name = enemyName;
            Status = new CharacterStatus(aScore, 10000, new List<Skill.SkillID> { Skill.SkillID.NormalAttack }, new List<Skill.SkillID> { });
            commandSelect = new DefaultCharacterCommandSelect(CharacterIndex);
            BCGraphics = new BattleCharaGraphics(this, statusPos, charaPos);
        }
    }
}
