using Soleil.Battle;
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
        public AbilityScore Score { get; private set; }
        readonly public EquipSet Equip;

        public Person(AbilityScore _score)
        {
            Score = _score;
            Equip = new EquipSet();
        }
    }
}
