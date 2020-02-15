using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class EnemyCharacter : Character
    {
        public EnemyCharacter(string enemyName, CharacterType cType)
            : base(-1, cType)
        {
            //てきとう
            var aScore = new AbilityScore(100, 10, 10, 10, 10, 10);
            Name = enemyName;
            Status = new CharacterStatus(aScore, 10000, new List<Skill.SkillID> { Skill.SkillID.NormalAttack }, new List<Skill.SkillID> { });

        }

        public EnemyCharacter Generate(int index, Vector statusPos, Vector charaPos)
        {
            var tmp = (EnemyCharacter)Generate(index);
            tmp.commandSelect = new DefaultCharacterCommandSelect(index);
            tmp.BCGraphics = new BattleCharaGraphics(this, statusPos, charaPos);
            return tmp;
        }
    }
}
