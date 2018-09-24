using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Item
{
    struct ItemData
    {
        public ItemID ID;
        public ItemType Type;
        public bool OnMenu;
        public bool OnBattle;
        public string Name;
        public string Description;
        public int EffectValue; // 効果量（攻撃力，回復量など）
        public ItemData(ItemID iD, ItemType type, bool usedOnMenu,
            bool usedOnBattle, int effectVal, string name, string description)
        {
            ID = iD;
            Type = type;
            OnMenu = usedOnMenu;
            OnBattle = usedOnBattle;
            EffectValue = effectVal;
            Name = name;
            Description = description;
        }
    }
}
