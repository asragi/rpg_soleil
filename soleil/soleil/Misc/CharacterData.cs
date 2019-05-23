using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    struct CharacterData
    {
        public string Name;
        public AbilityScore InitialScore;
        public int[] GainRate; // パラメータ成長率
        public CharacterData(string name, AbilityScore init)
        {
            GainRate = new int[4];
            (Name, InitialScore) = (name, init);
        }
    }

    static class CharacterDataBase
    {
        static CharacterData[] data;
        static CharacterDataBase()
        {
            data = SetData();

            CharacterData[] SetData()
            {
                var d = new CharacterData[(int)CharaName.size];
                d[(int)CharaName.Lune] = new CharacterData("ルーネ", new AbilityScore());
                d[(int)CharaName.Sunny] = new CharacterData("サニィ", new AbilityScore());
                return d;
            }
        }
    }
}
