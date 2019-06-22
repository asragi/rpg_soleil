﻿using Soleil.Battle;
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
        readonly public EquipSet Equip;
        readonly public SkillHolder Skill;
        public bool InParty;

        public Person(CharaName name, AbilityScore _score, SkillHolder skill)
        {
            Name = name;
            score = _score;
            Equip = new EquipSet();
            Skill = skill;
            InParty = true; // debug
        }

        public void RecoverHP(int val) => score.HP += val;
        public void RecoverMP(int val) => score.MP += val;
    }
}
