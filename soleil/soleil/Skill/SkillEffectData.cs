using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    /// <summary>
    /// メニュー画面で魔法発動した時の効果(ほんまにこのやり方でいいのかはわからない)
    /// </summary>
    static class SkillEffectData
    {
        /// <summary>
        /// メニューでスキル発動した時の効果群
        /// </summary>
        /// <param name="commander">術者</param>
        /// <param name="person">対象(複数対象の場合配列?)</param>
        /// <param name="id">ID</param>
        public static bool UseOnMenu(Person commander,Person[] person,SkillID id)
        {
            #region SKILLDATA
            var data = SkillDataBase.Get(id);
            if (id == SkillID.MagicalHeal)
            {
                return CommonOperation.Recover(commander, person[0], 60, data.Cost);
            }
            return false;
            #endregion
        }
    }
}
