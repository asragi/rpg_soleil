using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Item
{
    /// <summary>
    /// アイテム全てに共通するパラメータ．
    /// </summary>
    interface IItem
    {
        ItemID ID { get; }
        ItemType Type { get; }
        bool OnMenu { get; }
        bool OnBattle { get; }
        string Name { get; }
        string Description { get; }
    }

    /// <summary>
    /// 装備できるもののインターフェイス．
    /// </summary>
    interface IWearable
    {
        // 上昇・低下するキャラクターステータスのデータなど
    }

    /// <summary>
    /// 防具，アクセサリーに共通するパラメータ．
    /// </summary>
    interface IArmor : IWearable
    {
        DefData DefData { get; }
    }

    /// <summary>
    /// 防御力に関する構造体．
    /// </summary>
    struct DefData
    {
        public int pDef;
        public int mDef;
        public AttrDef pAttr;
        public AttrDef mAttr;
    }

    /// <summary>
    /// 属性防御に関する構造体．0を基準値として計算する．
    /// </summary>
    struct AttrDef
    {
        public int Heat;
        public int Thunder;
        public int Ice;
        public int Blow;
        public int Slash;
        public int Thrust;
        public AttrDef(int heat=0, int thunder = 0, int ice = 0, int blow = 0, int slash = 0, int thrust = 0)
        {
            Heat = heat;
            Thunder = thunder;
            Ice = ice;
            Blow = blow;
            Slash = slash;
            Thrust = thrust;
        }
    }

    /// <summary>
    /// 通常のアイテム．
    /// </summary>
    struct NormalItem : IItem
    {
        public ItemID ID { get; }
        public ItemType Type { get { return ItemType.Consumable; } }
        public bool OnMenu { get; }
        public bool OnBattle { get; }
        public string Name { get; }
        public string Description { get; }
        public int EffectValue; // 効果量（攻撃力，回復量など）
        public NormalItem(ItemID iD, bool usedOnMenu,
            bool usedOnBattle, int effectVal, string name, string description)
        {
            ID = iD;
            OnMenu = usedOnMenu;
            OnBattle = usedOnBattle;
            EffectValue = effectVal;
            Name = name;
            Description = description;
        }
    }

    struct WeaponData : IItem, IWearable
    {
        public ItemID ID { get; }
        public ItemType Type { get { return ItemType.Weapon; } }
        public bool OnMenu { get { return false; } }
        public bool OnBattle { get { return false; } }
        public string Name { get; }
        public string Description { get; }
    }

    struct ArmorData : IItem, IArmor
    {
        public ItemID ID { get; }
        public ItemType Type { get { return ItemType.Armor; } }
        public bool OnMenu { get { return false; } }
        public bool OnBattle { get { return false; } }
        public string Name { get; }
        public string Description { get; }

        public DefData DefData { get; }

        public ArmorData(ItemID iD, DefData def, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
            DefData = def;
        }
    }

    struct AccessaryData : IItem, IArmor
    {
        public ItemID ID { get; }
        public ItemType Type { get { return ItemType.Accessory; } }
        public bool OnMenu { get { return false; } }
        public bool OnBattle { get { return false; } }
        public string Name { get; }
        public string Description { get; }

        public DefData DefData { get; }

        public AccessaryData (ItemID iD, DefData def, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
            DefData = def;
        }
    }
}
