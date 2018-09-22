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
    static class ItemList
    {
        static Dictionary<ItemID, int> itemPossessMap;
        static ItemList()
        {
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
        public static bool HasItem(ItemID id) => itemPossessMap[id] > 0;

        /// <summary>
        /// 当該アイテムの所持数を返す．
        /// </summary>
        public static int HasItemNum(ItemID id) => itemPossessMap[id];

        /// <summary>
        /// 指定された個数アイテムを減らす．足りない場合は消費せずfalseを返す．
        /// </summary>
        public static bool Comsume(ItemID id, int num = 1)
        {
            if(itemPossessMap[id] >= num)
            {
                itemPossessMap[id] -= num;
                return true;
            }
            return false;
        }
    }
}
