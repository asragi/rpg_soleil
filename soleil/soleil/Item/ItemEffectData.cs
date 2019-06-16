using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Item
{
    enum ItemEffectType
    {
        Recover,
        Special,
        size,
    }

    /// <summary>
    /// アイテム使用効果の設定
    /// </summary>
    static class ItemEffectData
    {
        readonly static Dictionary<ItemID, System.Action<Person>> effectDictionary;

        static ItemEffectData()
        {

        }

        public static void UseOnMenu(Person p, ItemID itemID)
        {

        }

        public static void UseOnMenu(PersonParty party, ItemID id)
        {

        }
    }
}
