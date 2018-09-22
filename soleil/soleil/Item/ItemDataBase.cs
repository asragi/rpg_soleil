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
        // 消費アイテム 使いやすいように上の方にソートしたい
        Portion,
        Zarigani,
        // 非消費アイテム
        Stone,
        // 装備
        TestSword,

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
            Set("傷薬", ItemID.Portion, ItemType.Consumable, true, true, "味方一人を少量回復．");
            Set("活きのいいザリガニ", ItemID.Zarigani, ItemType.Consumable, true, true, "食べる......？");
            Set("石ころ", ItemID.Stone, ItemType.Unconsumable, "そこら辺の石ころ．");
            Set("デバッグソード", ItemID.TestSword, ItemType.Weapon, "デバッグ用ソード");
        }

        static void Set(String name, ItemID id, ItemType type, bool menu, bool battle, string desc)
        {
            data[(int)id] = new ItemData(id, type, menu, battle, name, desc);
        }
        
        // 装備など自明に使用不可能なもの用のset
        static void Set(String name, ItemID id, ItemType type, String desc)
        {
            data[(int)id] = new ItemData(id, type, false, false, name, desc);
        }
    }
}
