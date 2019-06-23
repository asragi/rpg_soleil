using Soleil.Battle;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    /// <summary>
    /// 装備交換時に表示されるウィンドウのクラス．
    /// </summary>
    class EquipItemList: BasicMenu
    {
        public bool Active;
        protected override Vector WindowPos => new Vector(550, 100);

        EquipDisplay equipDisplay;

        // 交換対象としている装備セットとアイテム種類
        private EquipSet equip;
        private ItemType itemType;
        private int accessaryIndex;

        public EquipItemList(EquipDisplay parent, MenuDescription desc)
            : base(parent, desc)
        {
            Init();
            equipDisplay = parent;
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            // 初めて呼ばれるまでのプレースホルダ
            if (equip == null) return new SelectablePanel[0];
            var result = new List<ItemPanel>();
            var bag = PlayerBaggage.GetInstance().Items;
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                ItemID id = (ItemID)i;
                if (!bag.HasItem(id)) continue;
                var data = ItemDataBase.Get(id);
                if (data.Type != itemType) continue;
                result.Add(new ItemPanel(id, bag, this));
            }
            return result.ToArray();
        }

        public void CallWithData(EquipSet set, ItemType item, int _accessaryindex = 0)
        {
            Active = true;
            equip = set;
            itemType = item;
            accessaryIndex = _accessaryindex;
            Init();
            Call();
        }

        public override void Quit()
        {
            base.Quit();
            Active = false;
        }

        public override void OnInputSubmit()
        {
            base.OnInputSubmit();
            if (SelectedPanel is EmptyPanel)
            {
                // Play buzzer sound
                return;
            }
            // 装備を変更する
            ItemID equipingID = ((ItemPanelBase)SelectedPanel).ID;
            ItemID removedItem = 0;
            var bag = PlayerBaggage.GetInstance().Items;
            if (itemType == ItemType.Weapon)
            {
                removedItem = equip.ChangeWeapon(equipingID);
            }
            if (itemType == ItemType.Armor)
            {
                removedItem = equip.ChangeArmor(equipingID);
            }
            if (itemType == ItemType.Accessory)
            {
                removedItem = equip.ChangeAccessary(equipingID, accessaryIndex);
            }
            bag.AddItem(removedItem);
            bag.Consume(equipingID);
            equipDisplay.RefreshEquipText();
            Quit();
        }
    }
}
