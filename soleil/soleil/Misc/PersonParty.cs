﻿using Soleil.Misc;
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

        public PersonParty()
        {
            allCharacters = new Person[(int)CharaName.size];
            ReadData(); // debug
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

        public void ReadData()
        {
            // TODO: セーブデータから読み込み
            var a = new AbilityScore(72, 189, 5, 5, 44, 5);
            var b = new AbilityScore(228, 121, 22, 18, 22, 20);
            var c = new AbilityScore(164, 82, 13, 24, 12, 14);
            var a_s = new SkillHolder(SkillID.Thunder, SkillID.Explode, SkillID.MagicalHeal);
            var b_s = new SkillHolder(SkillID.PointFlare, SkillID.Thunder, SkillID.Headbutt);
            var c_s = new SkillHolder(SkillID.Headbutt, SkillID.Sonicboom);
            b_s.LearnSkill(Skill.SkillID.MagicalHeal);
            allCharacters[(int)CharaName.Lune] = new Person(CharaName.Lune, a, a_s);
            allCharacters[(int)CharaName.Sunny] = new Person(CharaName.Sunny, b, b_s);
            allCharacters[(int)CharaName.Tella] = new Person(CharaName.Tella, c, c_s);
        }
    }
}
