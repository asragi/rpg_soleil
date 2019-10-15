using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// 攻撃属性
    /// 
    /// None : 無属性
    /// Beat : 打属性
    /// Cut : 斬属性
    /// Thrust : 突属性
    /// Fever : 熱属性
    /// Ice : 冷気属性
    /// Electro : 電撃属性
    /// </summary>
    enum AttackAttribution
    {
        None = -1, //無属性
        Beat, //打属性
        Cut, //斬属性
        Thrust, //突属性
        Fever, //熱属性
        Ice, //冷気属性
        Electro, //電撃属性
        size,
    }

    static class ExtendAttackAttribution
    {
        static readonly Dictionary<AttackAttribution, String> dict = new Dictionary<AttackAttribution, string>
        {
            {AttackAttribution.Beat, "打撃"},
            {AttackAttribution.Cut, "斬撃"},
            {AttackAttribution.Thrust, "弾丸"},
            {AttackAttribution.Fever, "熱"},
            {AttackAttribution.Ice, "冷気"},
            {AttackAttribution.Electro, "電撃"},
        };

        public static String Name(this AttackAttribution att) => dict[att];
    }
}
