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
        public static bool UseOnMenu(Person p, ItemID itemID)
        {
            #region ITEMDATA
            if (itemID == ItemID.Portion) return CommonOperation.RecoverByRate(p, 30, 0);
            return false;
            #endregion
        }

        public static bool UseOnMenu(PersonParty party, ItemID id)
        {
            return false;
        }
    }
}
