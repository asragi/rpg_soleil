using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    /// <summary>
    /// スキルを習得するレベルを保管します
    /// </summary>
    static class SkillLearn
    {
        /// <summary>
        /// MagicCategoryのレベルで習得するスキルを保管します
        /// </summary>
        public static readonly Dictionary<MagicCategory, Dictionary<int, SkillID>> skillLearn;
        static SkillLearn()
        {
            skillLearn = new Dictionary<MagicCategory, Dictionary<int, SkillID>>();
            for (int i = 0; i < (int)MagicCategory.size; ++i)
            {
                skillLearn.Add((MagicCategory)i, new Dictionary<int, SkillID>());
            }
            // skillLeanにデータ追加
            // sun
            Set(1, SkillID.PointFlare);
            Set(3, SkillID.WarmHeal);
            Set(5, SkillID.HeatWave);
            // shade
            Set(1, SkillID.Freeze);
            Set(3, SkillID.Bind);
            Set(5, SkillID.CoolDown);
            // magic
            Set(1, SkillID.Thunder);
            Set(3, SkillID.MagicalHeal);
            Set(5, SkillID.Explode);
            // dark
            Set(1, SkillID.Reaper);
            Set(3, SkillID.IlFlood);
            Set(5, SkillID.LifeSteal);
            // sound
            Set(1, SkillID.Sonicboom);
            Set(3, SkillID.Noize);
            Set(9, SkillID.Maximizer);
            // ninja
            Set(1, SkillID.Poizon);
            Set(3, SkillID.ArmorBreak);
            Set(5, SkillID.MirrorShade);
            // wood
            Set(1, SkillID.Relax);
            Set(3, SkillID.Fragrance);
            Set(5, SkillID.AlomaDrop);
            // metal
            Set(1, SkillID.Alchem);
            Set(3, SkillID.PileBunker);
            Set(5, SkillID.MetalCoat);
            // space
            Set(1, SkillID.Teleport);
            Set(3, SkillID.DimensionKill);
            Set(5, SkillID.SeaventhHeaven);
            // time
            Set(1, SkillID.Delay);
            Set(3, SkillID.Haste);
            Set(9, SkillID.HeavensDrive);

            // skillLearnへのデータ追加に使用します
            void Set(int lv, SkillID skill)
            {
                var data = (MagicData)SkillDataBase.Get(skill);
                skillLearn[data.Category].Add(lv, skill);
            }
        }
        /// <summary>
        /// MagicCategoryのレベルで覚えるスキルidを返します
        /// </summary>
        public static SkillID GetLearnBy(MagicCategory category, int lv) => skillLearn[category][lv];

        /// <summary>
        /// MagicCategoryのレベルで覚えるスキルの有無を返します
        /// </summary>
        public static bool ExistLearningSkill(MagicCategory category, int lv) => skillLearn[category].ContainsKey(lv);
    }
}
