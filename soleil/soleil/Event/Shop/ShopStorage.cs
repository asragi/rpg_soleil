using Soleil.Item;
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

        public void OnNextDay() => items.ForEach2(item => item.OnNextDay());

        public void Purchase(int index, int num = 1)
        {
            items[index].Purchase(num);
        }

        public bool IsSoldOut(int index) => items[index].IsSoldOut;
        public int GetStockNum(int index) => items[index].Stock;

        public Dictionary<ItemID, int> GetDict()
        {
            var result = new Dictionary<ItemID, int>();
            foreach (var item in items)
            {
                result.Add(item.ID, item.Value);
            }
            return result;
        }
    }
}
