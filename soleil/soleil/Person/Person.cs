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
        private readonly GrowthType[] growthTypes;
        public AbilityScore Score 
            => score;
        public int Lv { get; private set; }
        readonly public EquipSet Equip;
        readonly public SkillHolder Skill;
        public MagicLv Magic;
        public bool InParty;

        /// <summary>
        /// HPMP情報を含めてセーブデータから復元する場合のコンストラクタ．
        /// </summary>
        public Person(
            CharaName name, int lv, AbilityScore _score, SkillHolder skill, MagicLv magicLv,
            EquipSet equip)
        {
            Name = name;
            Lv = lv;
            score = _score;
            Equip = equip;
            Skill = skill;
            Magic = magicLv;
            InParty = true; // debug
        }

        /// <summary>
        /// 新規セーブデータでのコンストラクタ．
        /// </summary>
        public Person(CharaName name)
        {
            Name = name;
            Lv = 1;
            var data = PersonDatabase.Get(name);
            var initScore = data.InitScore;
            var lastScore = data.LastScore;
            growthTypes = data.Growth;
            score = GrowthParams.GetParamsByLv(growthTypes, initScore, lastScore, Lv);
            Equip = new EquipSet();
            Skill = new SkillHolder(data.InitSkill);
            Magic = new MagicLv(data.InitMagicExp, Skill);
            InParty = true; // debug
            LvUp(57); // debug
        }

        public void LvUp(int plus)
        {
            Lv = Math.Min(99, Lv + plus);
            // HP MP shouldn't recover
            int hp = score.HP;
            int mp = score.MP;
            var initScore = PersonDatabase.Get(Name).InitScore;
            var lastScore = PersonDatabase.Get(Name).LastScore;
            var tmp = GrowthParams.GetParamsByLv(growthTypes, initScore, lastScore, Lv);
            tmp.HP = hp;
            tmp.MP = mp;
            score = tmp;
        }

        public void RecoverHP(int val) => score.HP += val;
        public void RecoverMP(int val) => score.MP += val;
    }
}
