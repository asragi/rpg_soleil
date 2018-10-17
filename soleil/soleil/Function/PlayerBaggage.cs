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
        public ItemList Items;
        public MoneyWallet MoneyWallet;

        public PlayerBaggage()
        {
            Items = new ItemList();
            MoneyWallet = new MoneyWallet();
        }
    }
}
