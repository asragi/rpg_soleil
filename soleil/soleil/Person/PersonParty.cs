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
                Person[] result = new Person[(int)CharaName.size];
                result[(int)CharaName.Lune] = new Person(CharaName.Lune);
                result[(int)CharaName.Sunny] = new Person(CharaName.Sunny);
                result[(int)CharaName.Tella] = new Person(CharaName.Tella);
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
