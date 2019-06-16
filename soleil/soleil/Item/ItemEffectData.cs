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
            if (itemID == ItemID.Portion) return RecoverByRate(p, 30, 0);
            return false;

            bool RecoverByRate(Person target, double hpRate = 0, double mpRate = 0)
            {
                var hpRecoverRate = target.Score.HPMAX * (hpRate / 100);
                var mpRecoverRate = target.Score.MPMAX * (mpRate / 100);

                // Return false if using item is useless.
                // ***** WIP *****

                return true;
            }
        }

        public static void UseOnMenu(PersonParty party, ItemID id)
        {

        }
    }
}
