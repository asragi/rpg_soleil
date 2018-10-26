using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    /// <summary>
    /// アイテムに関する情報を取り扱うMenuPanel
    /// </summary>
    abstract class ItemPanelBase :SelectablePanel
    {
        public readonly ItemID ID;

        public ItemPanelBase(ItemID id, String name, BasicMenu parent)
            :base(name, parent)
        {
            ID = id;
        }
    }
}
