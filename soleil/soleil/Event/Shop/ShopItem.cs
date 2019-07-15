using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// Shopのアイテムに関して在庫数と補充状況を管理するクラス
    /// </summary>
    struct ShopItem
    {
        public readonly ItemID ID;
        public readonly int Value;
        // 小数で管理することで3日に2個補充されるなどを実現する
        private double netStock;
        private double supplyNum;
        private double itemMax;

        public ShopItem(ItemID id, int value, double initNum, double supply, double maxNum)
        {
            ID = id;
            Value = value;
            netStock = initNum;
            supplyNum = supply;
            itemMax = maxNum;
        }

        /// <summary>
        /// 在庫数
        /// </summary>
        public int Stock => (int)netStock;
        public bool IsSoldOut => netStock < 1;

        public void OnNextDay() => netStock = Math.Min(netStock + supplyNum, itemMax);
        public void Purchase(int num) => netStock = Math.Max(netStock - num, 0);
    }
}
