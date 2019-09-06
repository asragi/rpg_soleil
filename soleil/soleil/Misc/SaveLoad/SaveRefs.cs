using Soleil.Item;
using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    /// <summary>
    /// Save時に参照するインスタンスへの参照を一括で管理する．
    /// </summary>
    class SaveRefs
    {
        public PersonParty Party { get; set; }
        // Map
        public ObjectManager ObjectManager { get; set; }
        public MapManager MapManager { get; set; }
        // Items
        public ItemList ItemList { get; set; }
        public MoneyWallet MoneyWallet { get; set; }

    }
}
