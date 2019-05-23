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

        public int GetDef(AttackAttribution attr, string attackType)
        {
            var accessaryDef = Accessary.DefData.GetDefVal(attr, attackType);
            var def = Weapon.DefData.GetDefVal(attr, attackType) + Armor.DefData.GetDefVal(attr, attackType);
            return accessaryDef + def;
        }
    }
}
