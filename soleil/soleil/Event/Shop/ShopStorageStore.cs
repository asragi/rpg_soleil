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
        public ShopStorageStore GetInstance() => instance;
        private ShopStorageStore()
        {
            storages = new ShopStorage[(int)ShopName.size];
        }

        public void Register(ShopName name, ShopStorage storage)
        {
            storages[(int)name] = storage;
        }

        /// <summary>
        /// 登録されている情報を呼び出す．初回はnullを返す．
        /// </summary>
        public ShopStorage Get(ShopName name) => storages[(int)name];

        public void OnNextDay() => storages.ForEach2(storage => storage.OnNextDay());
    }
}
