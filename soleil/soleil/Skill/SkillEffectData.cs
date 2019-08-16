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
        /// <param name="person">対象</param>
        /// <param name="id">ID</param>
        public static bool UseOnMenu(Person commander,Person person,SkillID id)
        {
            #region SKILLDATA
            if (id == SkillID.MagicalHeal)
            {
                person.RecoverHP(60);
                commander.RecoverMP(-10);
                return true;
            }
            return false;
            #endregion
        }
    }
}
