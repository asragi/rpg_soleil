using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemMenu : BasicMenu, IListener
    {
        ItemList itemList;
        public ItemMenu(MenuComponent parent, MenuDescription desc)
            :base(parent, desc)
        {
            itemList = PlayerBaggage.GetInstance().Items;
            itemList.AddListener(this);
            // 描画すべきパネルを決定する．
            Init();
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var items = new List<SelectablePanel>();
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                if (!itemList.HasItem((ItemID)i)) continue;
                var data = ItemDataBase.Get((ItemID)i);
                items.Add(new ItemPanel((ItemID)i, itemList, this));
            }
            return items.ToArray();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            for (int i = 0; i < AllPanels.Length; i++)
            {
                AllPanels[i].Fade(FadeSpeed, MenuSystem.EaseFunc, true);
            }
        }

        public void OnListen(INotifier i)
        {
            if (i is ItemList) Refresh();
        }
    }
}
