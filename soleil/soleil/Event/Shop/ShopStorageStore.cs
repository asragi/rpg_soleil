using Soleil.Event.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// Shopごとの在庫状況を保持するクラス．
    /// </summary>
    class ShopStorageStore
    {
        private ShopStorage[] storages;
        private static ShopStorageStore instance = new ShopStorageStore();
        public static ShopStorageStore GetInstance() => instance;
        private ShopStorageStore()
        {
            storages = new ShopStorage[(int)ShopName.size];
        }

        /// <summary>
        /// 登録されている情報を呼び出す．
        /// </summary>
        public ShopStorage Get(ShopName name)
        {
            // 初回呼び出しなど作られてなければ作ってから返す．
            if (storages[(int)name] == null)
            {
                storages[(int)name] = new ShopStorage(ShopDatabase.Get(name));
            }
            return storages[(int)name];
        }

        public void OnNextDay() => storages.ForEach2(storage => storage.OnNextDay());
    }
}
