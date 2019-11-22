using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Item;
using Soleil.Skill;

namespace Soleil.Battle
{
    /// <summary>
    /// 装備品の装備状況を管理するクラス．
    /// </summary>
    class EquipSet
    {
        const int AccessarySize = 2;
        public WeaponData Weapon { get; set; }
        public ArmorData Armor { get; set; }
        public AccessaryData[] Accessary { get; set; }

        public EquipSet()
            :this(new[] { ItemID.WoodWand, ItemID.CottonShirt, ItemID.BeadsWork, ItemID.BeadsWork })
        { }

        public EquipSet(ItemID[] equipIDs)
        {
            Weapon = (WeaponData)ItemDataBase.Get(equipIDs[0]);
            Armor = (ArmorData)ItemDataBase.Get(equipIDs[1]);
            Accessary = new AccessaryData[AccessarySize];
            Accessary[0] = (AccessaryData)ItemDataBase.Get(equipIDs[2]);
            Accessary[1] = (AccessaryData)ItemDataBase.Get(equipIDs[3]);
        }

        public int GetDef(AttackAttribution attr, AttackType attackType)
        {
            int accessaryDef = 0;
            for (int i = 0; i < Accessary.Length; i++)
            {
                accessaryDef += Accessary[i].DefData.GetDefVal(attr, attackType);
            }
            var def = Weapon.DefData.GetDefVal(attr, attackType) + Armor.DefData.GetDefVal(attr, attackType);
            return accessaryDef + def;
        }

        public int GetAttack(AttackType t)
        {
            if (t == AttackType.Physical)
            {
                return Weapon.AttackData.Physical;
            }
            return Weapon.AttackData.Magical;
        }

        /// <summary>
        /// 武器を交換する．
        /// </summary>
        /// <param name="id">装備する武器のItemID</param>
        /// <returns>今まで装備していて交換によって外れる武器のID</returns>
        public ItemID ChangeWeapon(ItemID id)
        {
            var returnWeapon = Weapon;
            Weapon = (WeaponData)ItemDataBase.Get(id);
            return returnWeapon.ID;
        }

        /// <summary>
        /// 防具を交換する．
        /// </summary>
        /// <param name="id">装備する防具のItemID</param>
        /// <returns>今まで装備していて交換によって外れる防具のID</returns>
        public ItemID ChangeArmor(ItemID id)
        {
            var returnArmor = Armor;
            Armor = (ArmorData)ItemDataBase.Get(id);
            return returnArmor.ID;
        }

        /// <summary>
        /// アクセサリを交換する．
        /// </summary>
        /// <param name="id">装備するアクセサリのItemID</param>
        /// <returns>今まで装備していて交換によって外れるアクセサリのID</returns>
        public ItemID ChangeAccessary(ItemID id, int index)
        {
            var returnAccessary = Accessary[index];
            Accessary[index] = (AccessaryData)ItemDataBase.Get(id);
            return returnAccessary.ID;
        }

        public IItem[] GetEquipDataSet()
        {
            var result = new IItem[4];
            result[0] = Weapon;
            result[1] = Armor;
            result[2] = Accessary[0];
            result[3] = Accessary[1];
            return result;
        }
    }
}
