using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Soleil.Skill;
namespace Soleil.Battle
{
    class PlayableCharacter : Character
    {
        public PlayableCharacter(int index, AbilityScore aScore, Person person) : base(index)
        {
            Status = new CharacterStatus(aScore, 10000, GetMagicIDs(person.Skill), GetSkillIDs(person.Skill), person.Equip);
            commandSelect = new DefaultPlayableCharacterCommandSelect(charaIndex, Status);
        }

        /// <summary>
        /// 所有していてかつMagicに分類されるSkillIDを抽出する
        /// </summary>
        static List<SkillID> GetMagicIDs(SkillHolder sHolder)
        {
            return Enumerable.Range(0, (int)SkillID.size)
                .Select(i => (SkillID)i)
                .Where(id => sHolder.HasSkill(id) && SkillDataBase.Get(id) is MagicData)
                .ToList();
        }

        /// <summary>
        /// 所有していてかつSkillに分類されるSkillIDを抽出する
        /// </summary>
        static List<SkillID> GetSkillIDs(SkillHolder sHolder)
        {
            return Enumerable.Range(0, (int)SkillID.size)
                .Select(i => (SkillID)i)
                .Where(id => sHolder.HasSkill(id) && SkillDataBase.Get(id) is SkillData)
                .ToList();
        }

    }
}
