using Soleil.Battle;
using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// キャラクターのデータや装備品をまとめるクラス
    /// </summary>
    class Person
    {
        readonly public CharaName Name;
        public AbilityScore Score { get; private set; }
        readonly public EquipSet Equip;
        public bool InParty;

        public Person(CharaName name, AbilityScore _score)
        {
            Name = name;
            Score = _score;
            Equip = new EquipSet();
            InParty = true; // debug
        }
    }
}
