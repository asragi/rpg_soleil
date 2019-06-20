using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Item
{
    enum ItemType
    {
        Consumable,
        Unconsumable,
        Weapon,
        Armor,
        Accessory,
    }

    enum ItemID
    {
        // Empty（使用不可能アイテム）
        Empty,
        // 消費アイテム 使いやすいように上の方にソートしたい
        Portion,
        Zarigani,
        // 非消費アイテム
        Stone,
        // 装備
        // 武器
        SilverWand,
        // 防具
        Uniform,
        // アクセサリー
        BeadsWork, // ビーズのアクセサリー

        // Debug
        d0,
        d1,
        d2,
        d3,
        d4,
        d5,
        d6,
        d7,
        size,
    }

    static class ItemDataBase
    {
        static IItem[] data;
        static ItemDataBase()
        {
            data = new IItem[(int)ItemID.size];
            SetData();
        }

        static void SetData()
        {
            Set("石ころ", ItemID.Stone, 0, "そこら辺の石ころ．");

            // 使用可能アイテム
            SetUse("傷薬", ItemID.Portion, ItemTarget.OneAlly, "味方一人を少量回復．", false);
            SetUse("活きのいいザリガニ", ItemID.Zarigani, ItemTarget.OneAlly, "食べる......？");
            // 武器
            SetWeapon("シルバーワンド", ItemID.SilverWand, new AttackData(24, 30), null, null, "高級な魔法杖");
            // 防具
            SetArmor("ユニフォーム", ItemID.Uniform, new DefData(3, 8, null, new AttrDef(ice: 10, blow: 10)), null, "制服をリメイクしたもの．");
            // アクセサリー
            SetAc("ビーズのアクセサリー", ItemID.BeadsWork, new DefData(1, 5, null, null), null, "手作りの可愛いアクセサリー");

            // Debug
            for (int i = (int)ItemID.d0; i < 1 + (int)ItemID.d7; i++)
            {
                Set(((ItemID)i).ToString(), (ItemID)i, 50, "テスト" + i);
            }
        }

        static void Set(String name, ItemID id, bool menu, bool battle, int effectVal, string desc)
        {
            data[(int)id] = new NormalItem(id, menu, battle, effectVal, name, desc);
        }
        
        // 装備など自明に使用不可能なもの用のset
        static void Set(String name, ItemID id, int effectVal, String desc)
        {
            data[(int)id] = new NormalItem(id, false, false, effectVal, name, desc);
        }

        /// <summary>
        /// 使用可能なアイテム．
        /// </summary>
        static void SetUse(string name, ItemID id, ItemTarget target, string desc, bool onMenu = true, bool onBattle = true)
        {
            data[(int)id] = new ConsumableItem(id, name, desc, target, onMenu, onBattle);
        }

        static void SetWeapon(string name, ItemID id, AttackData attack, DefData? def, AbilityScore? score, String desc)
        {
            data[(int)id] = new WeaponData(id, attack, def, score, name, desc);
        }

        static void SetArmor(string name, ItemID id, DefData def, AbilityScore? score, string desc)
        {
            data[(int)id] = new ArmorData(id, def, score, name, desc);
        }

        static void SetAc(string name, ItemID id, DefData def, AbilityScore? score, String desc)
        {
            data[(int)id] = new AccessaryData(id, def, score, name, desc);
        }

        public static IItem Get(ItemID id) => data[(int)id];
    }
}
