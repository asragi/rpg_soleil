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
            Set("傷薬", ItemID.Portion, true, true, 30, "味方一人を少量回復．");
            Set("活きのいいザリガニ", ItemID.Zarigani, true, true, 20, "食べる......？");
            Set("石ころ", ItemID.Stone, 0, "そこら辺の石ころ．");

            // 武器
            SetWeapon("シルバーワンド", ItemID.SilverWand, new AttackData(24, 30), null, null, "高級な魔法杖");
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

        static void SetWeapon(String name, ItemID id, AttackData attack, DefData? def, AbilityScore? score, String desc)
        {
            data[(int)id] = new WeaponData(id, attack, def, score, name, desc);
        }

        static void SetAc(String name, ItemID id, DefData def, AbilityScore? score, String desc)
        {
            data[(int)id] = new AccessaryData(id, def, score, name, desc);
        }

        public static IItem Get(ItemID id) => data[(int)id];
    }
}
