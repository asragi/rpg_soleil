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
                var targetScore = target.Score;
                int hpRecoverVal = (int)(targetScore.HPMAX * (hpRate / 100));
                int mpRecoverVal = (int)(targetScore.MPMAX * (mpRate / 100));

                // Return false if using item is useless.
                bool fullHP = targetScore.HP == targetScore.HPMAX;
                bool fullMP = targetScore.MP == targetScore.MPMAX;
                bool errorCurable = false; // 状態異常がある && アイテム使用で回復可能
                if (fullHP && fullMP && !errorCurable) return false;
                if (fullHP && mpRecoverVal == 0 && !errorCurable) return false;
                if (fullMP && hpRecoverVal == 0 && !errorCurable) return false;

                // 回復処理
                if (hpRecoverVal > 0) target.RecoverHP(hpRecoverVal);
                if (mpRecoverVal > 0) target.RecoverMP(mpRecoverVal);
                return true;
            }
        }

        public static void UseOnMenu(PersonParty party, ItemID id)
        {

        }
    }
}
