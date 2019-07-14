using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event.Shop
{

    enum ShopName
    {
        Accessary,
        size
    }

    /// <summary>
    /// Shopのデータを設定しShopStorageを生み出すクラス．
    /// </summary>
    static class ShopDatabase
    {
        static Dictionary<ShopName, ShopItem[]> dict;

        static ShopDatabase()
        {
            dict = new Dictionary<ShopName, ShopItem[]>();

            dict.Add(ShopName.Accessary, new[]
            {
                new ShopItem(ItemID.Stone, 200, 10, 1, 20),
                new ShopItem(ItemID.Zarigani, 1200, 1, 0.5, 3),
                new ShopItem(ItemID.SilverWand, 74000, 1, 0, 1),
                new ShopItem(ItemID.BeadsWork, 3000, 2, 0.3, 10)
            });
        }

        public static ShopItem[] Get(ShopName name) => dict[name];
    }
}
