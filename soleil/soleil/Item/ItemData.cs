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
        public ItemData(ItemID iD, ItemType type, bool usedOnMenu, bool usedOnBattle, string name, string description)
        {
            ID = iD;
            Type = type;
            OnMenu = usedOnMenu;
            OnBattle = usedOnBattle;
            Name = name;
            Description = description;
        }
    }
}
