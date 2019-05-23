using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    class PersonParty
    {
        Person[] allCharacters;

        public PersonParty()
        {
            allCharacters = new Person[(int)CharaName.size];
        }

        public Person Get(CharaName name)
        {
            return allCharacters[(int)name];
        }
    }
}
