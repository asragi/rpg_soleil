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
        int initIndex = 0;
        public ItemMenu(MenuComponent parent, MenuDescription desc)
            :base(parent, desc)
        {
            itemList = PlayerBaggage.GetInstance().Items;
            // 所持しているすべてのアイテムの表示用パネルを生成
            AllPanels = MakeAllItemPanels().ToArray();
            // 描画すべきパネルを決定する．
            Panels = SetPanels();
        }

        private List<ItemPanel> MakeAllItemPanels()
        {
            var items = new List<ItemPanel>();
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                if (!itemList.HasItem((ItemID)i)) continue;
                var data = ItemDataBase.Get((ItemID)i);
                items.Add(new ItemPanel((ItemID)i, itemList, this));
            }
            return items;
        }

        private SelectablePanel[] SetPanels()
        {
            if (AllPanels.Length <= 0) // アイテムを一つも持っていない
            {
                IndexSize = 1;
                return new[] { new ItemPanel(ItemID.Empty, itemList, this) };
            }
            var panelSize = Math.Min(AllPanels.Length, RowSize);
            IndexSize = panelSize;

            var tmp = new SelectablePanel[panelSize];
            for (int i = 0; i < panelSize; ++i)
            {
                tmp[i] = AllPanels[initIndex + i];
                tmp[i].LocalPos = ItemDrawStartPos + new Vector(0, (tmp[i].PanelSize.Y + ItemPanelSpacing) * i);
            }
            return tmp;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            for (int i = 0; i < AllPanels.Length; i++)
            {
                AllPanels[i].Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            }
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
                    initIndex = Math.Max(0, AllPanels.Length - RowSize);
                    Index = Math.Min(AllPanels.Length, Panels.Length) - 1;
                }
                Panels = SetPanels();
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
                if(initIndex < AllPanels.Length - RowSize)
                {
                    initIndex++;
                    Index = Math.Min(AllPanels.Length, Panels.Length) - 1;
                }
                else
                {
                    initIndex = 0;
                    Index = 0;
                }
                Panels = SetPanels();
                RefreshSelected();
                return;
            }
            Index = (Index + 1 + IndexSize) % IndexSize;
            RefreshSelected();
        }
    }
}
