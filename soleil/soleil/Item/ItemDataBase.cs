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
        TestSword,

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
        static ItemData[] data;
        static ItemDataBase()
        {
            data = new ItemData[(int)ItemID.size];
            SetData();
        }

        static void SetData()
        {
            Set("傷薬", ItemID.Portion, ItemType.Consumable, true, true, 30, "味方一人を少量回復．");
            Set("活きのいいザリガニ", ItemID.Zarigani, ItemType.Consumable, true, true, 20, "食べる......？");
            Set("石ころ", ItemID.Stone, ItemType.Unconsumable, 0, "そこら辺の石ころ．");
            Set("デバッグソード", ItemID.TestSword, ItemType.Weapon, 50, "デバッグ用ソード");

            // Debug
            for (int i = (int)ItemID.d0; i < 1 + (int)ItemID.d7; i++)
            {
                Set(((ItemID)i).ToString(), (ItemID)i, ItemType.Unconsumable, 50, "テスト" + i);
            }
        }

        static void Set(String name, ItemID id, ItemType type, bool menu, bool battle, int effectVal, string desc)
        {
            data[(int)id] = new ItemData(id, type, menu, battle, effectVal, name, desc);
        }
        
        // 装備など自明に使用不可能なもの用のset
        static void Set(String name, ItemID id, ItemType type, int effectVal, String desc)
        {
            data[(int)id] = new ItemData(id, type, false, false, effectVal, name, desc);
        }

        public static ItemData Get(ItemID id) => data[(int)id];
    }
}
