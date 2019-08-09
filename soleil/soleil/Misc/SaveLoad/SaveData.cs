using Soleil.Battle;
using Soleil.Item;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    /// <summary>
    /// セーブデータをシリアライズ・デシリアライズするための構造体
    /// </summary>
    [Serializable]
    struct SaveData
    {
        CharacterData[] CharacterDatas { get; set; }

        public SaveData(PersonParty _party)
        {
            CharacterDatas = MakePartyData(_party);

            CharacterData[] MakePartyData(PersonParty party)
            {
                int length = (int)CharaName.size;
                var result = new CharacterData[length];
                for (int i = 0; i < length; i++)
                {
                    var person = party.Get((CharaName)i);
                    result[i] = new CharacterData(
                        person.Name, person.Lv, person.Equip, person.Score,
                        person.Skill, person.Magic
                    );
                }
                return result;
            }
        }

        public PersonParty GetParty()
        {
            int length = CharacterDatas.Length;
            Person[] people = new Person[length];
            for (int i = 0; i < length; i++)
            {
                people[i] = CharacterDatas[i].Get();
            }
            return new PersonParty(people);
        }

        [Serializable]
        struct CharacterData
        {
            CharaName Name { get; set; }
            EquipData Equip { get; set; }
            Parameter Param { get; set; }
            SkillFlag Skill { get; set; }
            int Lv { get; set; }
            MagicExp Magic { get; set; }

            public CharacterData(
                CharaName name, int lv, EquipSet _equip, AbilityScore _score,
                SkillHolder skill, MagicLv magic)
            {
                Name = name;
                Equip = new EquipData(_equip);
                Param = new Parameter(_score);
                Skill = new SkillFlag(skill);
                Lv = lv;
                Magic = new MagicExp(magic);
            }

            public Person Get()
            {
                var skill = Skill.Get();
                return new Person(Name, Lv, Param.Get(), skill, Magic.Get(skill), Equip.Get());
            }

            [Serializable]
            struct EquipData
            {
                public int WeaponID { get; set; }
                public int ArmorID { get; set; }
                public int[] Accessaries { get; set; }

                public EquipData(EquipSet _equip)
                {
                    WeaponID = (int)_equip.Weapon.ID;
                    ArmorID = (int)_equip.Armor.ID;
                    Accessaries = Array.ConvertAll(_equip.Accessary, s => (int)s.ID);
                }

                public EquipSet Get()
                {
                    var equip = new EquipSet();
                    equip.Weapon = (WeaponData)ItemDataBase.Get((ItemID)WeaponID);
                    equip.Armor = (ArmorData)ItemDataBase.Get((ItemID)ArmorID);
                    equip.Accessary = Array.ConvertAll(
                        Accessaries, s => (AccessaryData)ItemDataBase.Get((ItemID)s)
                    );
                    return equip;
                }
            }

            [Serializable]
            struct SkillFlag
            {
                public bool[] Flags { get; set; }

                public SkillFlag(SkillHolder skill)
                {
                    Flags = skill.GetArray();
                }

                public SkillHolder Get()
                {
                    return new SkillHolder(Flags);
                }
            }

            [Serializable]
            struct MagicExp
            {
                int[] exps;
                public MagicExp(MagicLv magic)
                {
                    int length = (int)MagicCategory.size;
                    exps = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        exps[i] = magic[(MagicCategory)i];
                    }
                }

                public MagicLv Get(SkillHolder skill)
                {
                    return new MagicLv(exps, skill);
                }
            }

            [Serializable]
            struct Parameter
            {
                public int HPMAX { get; set; }
                public int HP { get; set; }
                public int MPMAX { get; set; }
                public int MP { get; set; }
                public int STR { get; set; }
                public int VIT { get; set; }
                public int MAG { get; set; }
                public int SPD { get; set; }

                public Parameter(AbilityScore score)
                {
                    HPMAX = score.HPMAX;
                    HP = score.HP;
                    MPMAX = score.MPMAX;
                    MP = score.MP;
                    STR = score.STR;
                    VIT = score.VIT;
                    MAG = score.MAG;
                    SPD = score.SPD;
                }

                public AbilityScore Get()
                {
                    return new AbilityScore(HPMAX, MPMAX, STR, VIT, MAG, SPD) { HP = HP, MP = MP };
                }
            }
        }
    }
}
