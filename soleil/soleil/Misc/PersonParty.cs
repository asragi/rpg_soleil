using Soleil.Misc;
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

        public void ReadData()
        {
            // TODO: セーブデータから読み込み
            var a = new AbilityScore(70, 180, 5, 5, 44, 5);
            var b = new AbilityScore(220, 120, 22, 18, 22, 20);
            allCharacters[(int)CharaName.Lune] = new Person(CharaName.Lune, a);
            allCharacters[(int)CharaName.Sunny] = new Person(CharaName.Sunny, b);
        }
    }
}
