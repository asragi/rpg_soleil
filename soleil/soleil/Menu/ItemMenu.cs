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
        ItemTargetSelect itemTargetSelect;
        ItemList itemList;
        public ItemMenu(MenuComponent parent, MenuDescription desc)
            :base(parent, desc)
        {
            itemList = PlayerBaggage.GetInstance().Items;
            itemList.AddListener(this);
            // 描画すべきパネルを決定する．
            Init();
        }

        public void SetRefs(ItemTargetSelect its, StatusMenu sm)
        {
            itemTargetSelect = its;
            itemTargetSelect.SetRefs(sm);
        }

        protected override SelectablePanel[] MakeAllPanels()
        {
            var items = new List<TextSelectablePanel>();
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                if (!itemList.HasItem((ItemID)i)) continue;
                var data = ItemDataBase.Get((ItemID)i);
                var panel = new ItemPanel((ItemID)i, itemList, this, data.OnMenu);
                items.Add(panel);
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

        public override void OnInputSubmit()
        {
            base.OnInputSubmit();
            var nowPanel = (ItemPanel)Panels[Index];
            ItemEffect(nowPanel.ID);

            void ItemEffect(ItemID id)
            {
                var tmp = ItemDataBase.Get(id);
                if (!(tmp is ConsumableItem)) return; // Consumableでないなら終了; 「使用できる武器」みたいなのは必要に応じてまた．
                if (!tmp.OnMenu) return; // Menuで使用可能でないなら終了
                var item = (ConsumableItem)tmp;

                if (item.Target == ItemTarget.Nothing)
                {
                    Console.WriteLine("Event発生など");
                }else if (item.Target == ItemTarget.OneAlly)
                {
                    // inputをstatusに渡す．
                    itemTargetSelect.Call();
                    IsActive = false;
                    Quit();
                }else if (item.Target == ItemTarget.AllAlly)
                {
                    // inputをstatusに渡す．
                    Console.WriteLine("味方全員を対象");
                }
            }
        }

        // IListener
        public ListenerType Type { get { return ListenerType.ItemMenu; } }
        public void OnListen(INotifier i)
        {
            if (i is ItemList) Refresh();
        }
    }
}
