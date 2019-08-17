using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 回復等の処理を切り出したかった(いい名前は思い浮かばなかった)
    /// </summary>
    static class CommonOperation
    {
        /// <summary>
        /// commanderがtargetを回復 使用MPはmp
        /// </summary>
        public static bool Recover(Person commander,Person target,double force, int mp)
        {
            var comScore = commander.Score;
            var targetScore = target.Score;
            if (comScore.MP < mp) return false;
            if (targetScore.HP == targetScore.HPMAX) return false;
            //とりあえず状態異常は気にしない
            commander.RecoverMP(-mp);
            //回復量計算
            //b.MAXHP * (Force/100) * (a.MATK +a.MAG + b.VIT + 3) / 300 対象b
            double hl = targetScore.HPMAX * (force / 100.0);
            //MATKが何かわからなかった。漸次
            hl *= (double)(comScore.MAG+targetScore.VIT+3)/300.0;
            target.RecoverHP((int)hl);
            return true;
        }
        /// <summary>
        /// ItemEffectDataにあったものをこちらに輸送
        /// </summary>
        public static bool RecoverByRate(Person target, double hpRate = 0, double mpRate = 0)
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
}
