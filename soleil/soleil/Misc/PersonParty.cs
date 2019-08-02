using Soleil.Battle;
using Soleil.Misc;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class PersonParty
    {
        Person[] allCharacters;

        /// <summary>
        /// CreateNewParty
        /// </summary>
        public PersonParty()
        {
            allCharacters = CreateNewParty();

            Person[] CreateNewParty()
            {
                var result = new Person[(int)CharaName.size];
                var a = new AbilityScore(72, 189, 5, 5, 44, 5);
                var b = new AbilityScore(228, 121, 22, 18, 22, 20);
                var c = new AbilityScore(164, 82, 13, 24, 12, 14);
                var a_s = new SkillHolder();
                var b_s = new SkillHolder(SkillID.PointFlare, SkillID.Thunder, SkillID.Headbutt);
                var c_s = new SkillHolder(SkillID.Headbutt, SkillID.Sonicboom);
                var a_m = new MagicLv(a_s);
                for (int i = 0; i < (int)MagicCategory.size; i++)
                {
                    a_m.AddExp(100 - i * 10, (MagicCategory)i);
                }
                var b_m = new MagicLv(b_s);
                var c_m = new MagicLv(c_s);
                var a_eq = new EquipSet();
                var b_eq = new EquipSet();
                var c_eq = new EquipSet();
                result[(int)CharaName.Lune] = new Person(CharaName.Lune, a, a_s, a_m, a_eq);
                result[(int)CharaName.Sunny] = new Person(CharaName.Sunny, b, b_s, b_m, b_eq);
                result[(int)CharaName.Tella] = new Person(CharaName.Tella, c, c_s, c_m, c_eq);
                return result;
            }
        }

        /// <summary>
        /// CreateFromSaveData
        /// </summary>
        public PersonParty(Person[] people)
        {
            allCharacters = people;
        }

        public Person Get(CharaName name)
        {
            return allCharacters[(int)name];
        }

        public int GetPartyNum()
        {
            return 2;
        }

        public Person[] GetActiveMembers()
        {
            var result = new List<Person>();
            for (int i = 0; i < allCharacters.Length; i++)
            {
                var target = allCharacters[i];
                if (!target.InParty) continue;
                result.Add(target);
            }
            return result.ToArray();
        }
    }
}
