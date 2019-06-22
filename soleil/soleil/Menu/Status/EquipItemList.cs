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

        // 交換対象としている装備セットとアイテム種類
        private EquipSet equip;
        private ItemType itemType;

        public EquipItemList(MenuComponent parent, MenuDescription desc)
            : base(parent, desc) { Init(); }

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

        public void CallWithData(EquipSet set, ItemType item)
        {
            equip = set;
            itemType = item;
            Init();
            Call();
        }

        public override void Quit()
        {
            base.Quit();
            Active = false;
        }
    }
}
