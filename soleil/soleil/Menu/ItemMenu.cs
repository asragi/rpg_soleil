using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : BasicMenu
    {
        ItemList itemList;
        // 所持しているすべてのアイテムのパネル
        List<ItemPanel> allItemPanels;
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            // 実際は他のところでインスタンス生成して参照を受け取る．
            itemList = new ItemList();
            itemList.AddItem(ItemID.Portion);
            itemList.AddItem(ItemID.Zarigani);
            for (int i = (int)ItemID.d0; i < (int)ItemID.d7+1; i++)
            {
                itemList.AddItem((ItemID)i);
            }
            // 所持しているすべてのアイテムのパネル
            allItemPanels = new List<ItemPanel>();
            // 表示用のパネル
            Panels = new ItemPanel[RowSize];
            // 所持しているすべてのアイテムの表示用パネルを生成
            allItemPanels = RefreshPanels();

            for (int i = 0; i < RowSize; ++i)
            {
                if (allItemPanels.Count <= 0) // アイテムを一つも持っていない
                {
                    allItemPanels.Add(new ItemPanel("", -1, this));
                }
                if (allItemPanels.Count <= i) return;
                IndexSize = i + 1;
                Panels[i] = allItemPanels[i];
                Panels[i].LocalPos = ItemDrawStartPos + new Vector(0, (Panels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
        }

        private List<ItemPanel> RefreshPanels()
        {
            var items = new List<ItemPanel>();
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                if (!itemList.HasItem((ItemID)i)) continue;
                var data = ItemDataBase.Get((ItemID)i);
                items.Add(new ItemPanel(data.Name, itemList.GetItemNum((ItemID)i), this));
            }
            return items;
        }
    }
}
