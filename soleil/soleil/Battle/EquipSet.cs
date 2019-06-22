using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Item;

namespace Soleil.Battle
{
    /// <summary>
    /// 装備品の装備状況を管理するクラス．
    /// </summary>
    class EquipSet
    {
        public WeaponData Weapon { get; set; }
        public ArmorData Armor { get; set; }
        public AccessaryData Accessary { get; set; }

        public EquipSet()
        {
            Weapon = (WeaponData)ItemDataBase.Get(ItemID.OldWand);
            Armor = (ArmorData)ItemDataBase.Get(ItemID.Uniform);
            Accessary = (AccessaryData)ItemDataBase.Get(ItemID.BeadsWork);
        }

        public int GetDef(AttackAttribution attr, string attackType)
        {
            var accessaryDef = Accessary.DefData.GetDefVal(attr, attackType);
            var def = Weapon.DefData.GetDefVal(attr, attackType) + Armor.DefData.GetDefVal(attr, attackType);
            return accessaryDef + def;
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
        public ItemID ChangeAccessary(ItemID id)
        {
            var returnAccessary = Accessary;
            Accessary = (AccessaryData)ItemDataBase.Get(id);
            return returnAccessary.ID;
        }
    }
}
