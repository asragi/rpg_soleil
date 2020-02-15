using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    class ItemTargetSelect : StatusTargetSelectBase
    {
        ItemMenu itemMenu;

        // 使用情報
        ItemID id;
        ItemList itemList;

        public ItemTargetSelect(ItemMenu _parent)
            : base(_parent)
        {
            itemMenu = _parent;
        }

        public void SetWillUsedItem(ItemID _id, ItemList bag)
        {
            id = _id;
            itemList = bag;
        }

        public override void OnInputSubmit()
        {
            Person selected = StatusMenu.GetSelectedPerson();
            bool useSuccess = ItemEffectData.UseOnMenu(selected, id);
            if (useSuccess)
            {
                itemList.Consume(id);
                Audio.PlaySound(SoundID.DecideSoft);
                if (!itemList.HasItem(id)) OnInputCancel();
            }
            else
            {
                // Play buzzer sound to notify that item cannot be used.
            }
        }

        public override void OnInputCancel()
        {
            base.OnInputCancel();
            itemMenu.Call();
        }
    }
}
