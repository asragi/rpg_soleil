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
    /// <summary>
    /// キャラクターのデータや装備品をまとめるクラス
    /// </summary>
    class Person
    {
        readonly public CharaName Name;
        private AbilityScore score;
        public AbilityScore Score => score;
        public int Lv { get; private set; }
        readonly public EquipSet Equip;
        readonly public SkillHolder Skill;
        public MagicLv Magic;
        public bool InParty;

        public Person(
            CharaName name, AbilityScore _score, SkillHolder skill, MagicLv magicLv,
            EquipSet equip)
        {
            Name = name;
            score = _score;
            Equip = equip;
            Skill = skill;
            Magic = magicLv;
            InParty = true; // debug
        }

        public void RecoverHP(int val) => score.HP += val;
        public void RecoverMP(int val) => score.MP += val;
    }
}
