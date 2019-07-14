using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// Shopごとのアイテムの在庫状況を管理するクラス．
    /// </summary>
    class ShopStorage
    {
        ShopItem[] items;

        public ShopStorage(ShopItem[] _items)
        {
            items = _items;
        }
    }
}
