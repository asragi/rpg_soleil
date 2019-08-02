using Soleil.Battle;
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

            CharacterData[] MakePartyData(PersonParty party) {
                int length = (int)CharaName.size;
                var result = new CharacterData[length];
                for (int i = 0; i < length; i++)
                {
                    var person = party.Get((CharaName)i);
                    result[i] = new CharacterData(person.Equip, person.Score);
                }
                return result;
            }
        }

        struct CharacterData
        {
            EquipData Equip { get; set; }
            Parameter Param { get; set; }

            public CharacterData(EquipSet _equip, AbilityScore _score)
            {
                Equip = new EquipData(_equip);
                Param = new Parameter(_score);
            }

            struct EquipData
            {
                public int WeaponID { get; set; }
                public int ArmorID { get; set;  }
                public int[] Accessaries { get; set; }

                public EquipData(EquipSet _equip)
                {
                    WeaponID = (int)_equip.Weapon.ID;
                    ArmorID = (int)_equip.Armor.ID;
                    Accessaries = Array.ConvertAll(_equip.Accessary, s => (int)s.ID);
                }
            }

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
            }
        }
    }
}
