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
        AbilityScore AbilityScore { get; }
    }

    /// <summary>
    /// 防具，アクセサリーに共通するパラメータ．
    /// </summary>
    interface IArmor : IWearable
    {
        DefData DefData { get; }
    }

    /// <summary>
    /// 攻撃力に関するパラメータ．
    /// </summary>
    struct AttackData
    {
        public int Physical;
        public int Magical;
        
        public AttackData(int p, int m)
        {
            Physical = p;
            Magical = m;
        }
    }

    /// <summary>
    /// 防御力に関する構造体．
    /// </summary>
    struct DefData
    {
        public int Physical;
        public int Magical;
        public AttrDef PhysicalAttr;
        public AttrDef MagicalAttr;

        public DefData(int p, int m, AttrDef? _pAttr, AttrDef? _mAttr)
        {
            Physical = p;
            Magical = m;
            PhysicalAttr = _pAttr ?? new AttrDef();
            MagicalAttr = _mAttr ?? new AttrDef();
        }
    }

    /// <summary>
    /// 属性防御に関する構造体．0を基準値として計算する．
    /// </summary>
    struct AttrDef
    {
        public int[] Values;
        public AttrDef(int heat=0, int thunder = 0, int ice = 0, int blow = 0, int slash = 0, int thrust = 0)
        {
            Values = new int[(int)AttackAttribution.size];
            Values[(int)AttackAttribution.Fever] = heat;
            Values[(int)AttackAttribution.Electro] = thunder;
            Values[(int)AttackAttribution.Ice] = ice;
            Values[(int)AttackAttribution.Beat] = blow;
            Values[(int)AttackAttribution.Cut] = slash;
            Values[(int)AttackAttribution.Thrust] = thrust;
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

    /// <summary>
    /// 使用可能なアイテム
    /// </summary>
    struct ConsumableItem : IItem
    {
        public ItemID ID { get; }
        public ItemType Type => ItemType.Consumable;
        public bool OnMenu { get; }
        public bool OnBattle { get; }
        public string Name { get; }
        public string Description { get; }
        public ItemTarget Target { get; }

        public ConsumableItem(ItemID id, string name, string description, ItemTarget target, bool onMenu = true, bool onBattle = true)
        {
            (ID, Name, Description, Target, OnMenu, OnBattle) = (id, name, description, target, onMenu, onBattle);
        }
    }

    /// <summary>
    /// アイテム使用時の効果対象
    /// </summary>
    enum ItemTarget
    {
        OneAlly,
        AllAlly,
        Nothing,
    }

    struct WeaponData : IItem, IArmor
    {
        public ItemID ID { get; }
        public ItemType Type { get { return ItemType.Weapon; } }
        public bool OnMenu { get { return false; } }
        public bool OnBattle { get { return false; } }
        public string Name { get; }
        public string Description { get; }

        public DefData DefData { get; }
        public AttackData AttackData { get; }
        public AbilityScore AbilityScore { get; }

        public WeaponData(ItemID iD, AttackData attk, DefData? def, AbilityScore? score, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
            AttackData = attk;
            DefData = def ?? new DefData(0, 0, null, null);
            AbilityScore = score ?? new AbilityScore(0, 0, 0, 0, 0, 0);
        }
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
        public AbilityScore AbilityScore { get; }

        public ArmorData(ItemID iD, DefData def, AbilityScore? score, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
            DefData = def;
            AbilityScore = score ?? new AbilityScore(0, 0, 0, 0, 0, 0);
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
        public AbilityScore AbilityScore { get; }

        public AccessaryData (ItemID iD, DefData def, AbilityScore? score, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
            DefData = def;
            AbilityScore = score ?? new AbilityScore(0, 0, 0, 0, 0, 0);
        }
    }
}
