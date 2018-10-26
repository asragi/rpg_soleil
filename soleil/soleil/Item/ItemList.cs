using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Item
{
    /// <summary>
    /// プレイヤーが所持するアイテムの個数を管理する．
    /// </summary>
    class ItemList : INotifier
    {
        Dictionary<ItemID, int> itemPossessMap;
        List<IListener> listeners;

        public ItemList()
        {
            listeners = new List<IListener>();
            itemPossessMap = new Dictionary<ItemID, int>();
            for (int i = 0; i < (int)ItemID.size; i++)
            {
                // 全てのitemIDの所持数を0にセットする．
                itemPossessMap.Add((ItemID)i, 0);
            }
        }

        /// <summary>
        /// 当該アイテムを1つ以上所持しているかどうかを返す．
        /// </summary>
        public bool HasItem(ItemID id) => itemPossessMap[id] > 0;

        /// <summary>
        /// 当該アイテムの所持数を返す．
        /// </summary>
        public int GetItemNum(ItemID id) => itemPossessMap[id];

        /// <summary>
        /// 指定された個数アイテムを減らす．足りない場合は消費せずfalseを返す．
        /// </summary>
        public bool Consume(ItemID id, int num = 1)
        {
            if(itemPossessMap[id] >= num)
            {
                itemPossessMap[id] -= num;
                Refresh();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 指定された個数アイテムを増やす．
        /// </summary>
        public void AddItem(ItemID id, int num = 1)
        {
            itemPossessMap[id] += num;
            Refresh();
        }

        public void AddListener(IListener listener) => listeners.Add(listener);

        private void Refresh()
        {
            // アイテム所持数の更新を通知
            foreach (var item in listeners)
            {
                item.OnListen(this);
            }
        }
    }
}
