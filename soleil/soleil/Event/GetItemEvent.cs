using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    class GetItemEvent: EventBase
    {
        private readonly ItemID itemID;
        private readonly ToastMaster toastMaster;
        private int num;
        public GetItemEvent(ItemID id, ToastMaster tm, int _num = 1)
        {
            itemID = id;
            toastMaster = tm;
            num = _num;
        }

        public override void Execute()
        {
            base.Execute();
            // display Toast
            var itemData = ItemDataBase.Get(itemID);
            ItemType type = itemData.Type;
            TextureID icon = type.GetIcon();
            string name = itemData.Name;
            toastMaster.PopUpAlert(icon, name, num);
            // Add Item To Player Bag
            var itemBag = PlayerBaggage.GetInstance().Items;
            itemBag.AddItem(itemID, num);
            Next();
        }
    }
}
