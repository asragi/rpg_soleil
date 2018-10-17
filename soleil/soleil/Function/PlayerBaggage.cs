using Soleil.Item;
using Soleil.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 所持金・アイテムなどをまとめたクラス．ゲーム開始の度に廃棄・生成する．
    /// Sceneを跨いで参照する．
    /// </summary>
    class PlayerBaggage
    {
        private static PlayerBaggage playerBaggage = new PlayerBaggage();
        public static PlayerBaggage GetInstance() => playerBaggage;
        public ItemList Items;
        public MoneyWallet MoneyWallet;

        private PlayerBaggage(){ }

        public void SetData(ItemList itemList, MoneyWallet moneyWallet)
        {
            Items = itemList;
            MoneyWallet = moneyWallet;
        }
    }
}
