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
        int initIndex = 0;
        public ItemMenu(MenuComponent parent)
            :base(parent)
        {
            // 実際は他のところでインスタンス生成して参照を受け取る．
            itemList = new ItemList();
            // debug
            itemList.AddItem(ItemID.Portion);
            itemList.AddItem(ItemID.Zarigani);
            for (int i = (int)ItemID.d0; i < (int)ItemID.d7+1; i++)
            {
                itemList.AddItem((ItemID)i,i);
            }
            // 所持しているすべてのアイテムのパネル
            allItemPanels = new List<ItemPanel>();
            // 表示用のパネル
            Panels = new ItemPanel[RowSize];
            // 所持しているすべてのアイテムの表示用パネルを生成
            allItemPanels = MakeAllItemPanels();
            // 描画すべきパネルを決定する．
            SetPanels();
        }

        private List<ItemPanel> MakeAllItemPanels()
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

        private void SetPanels()
        {
            for (int i = 0; i < RowSize; ++i)
            {
                if (allItemPanels.Count <= 0) // アイテムを一つも持っていない
                {
                    allItemPanels.Add(new ItemPanel("", -1, this));
                }
                if (allItemPanels.Count <= i) return;
                IndexSize = i + 1;
                Panels[i] = allItemPanels[initIndex + i];
                Panels[i].LocalPos = ItemDrawStartPos + new Vector(0, (Panels[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            allItemPanels.ForEach(s => s.Fade(FadeSpeed, MenuSystem.EaseFunc, true));
        }

        public override void OnInputUp()
        {
            if (Index == 0)
            {
                if(initIndex > 0)
                {
                    initIndex--;
                    Index = 0;
                }
                else
                {
                    initIndex = Math.Max(0, allItemPanels.Count - RowSize);
                    Index = Math.Min(allItemPanels.Count, Panels.Length) - 1;
                }
                SetPanels();
                RefreshSelected();
                return;
            }
            Index = (Index - 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }

        public override void OnInputDown()
        {
            if (Index == RowSize-1)
            {
                if(initIndex < allItemPanels.Count - RowSize)
                {
                    initIndex++;
                    Index = Math.Min(allItemPanels.Count, Panels.Length) - 1;
                }
                else
                {
                    initIndex = 0;
                    Index = 0;
                }
                SetPanels();
                RefreshSelected();
                return;
            }
            Index = (Index + 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }
    }
}
