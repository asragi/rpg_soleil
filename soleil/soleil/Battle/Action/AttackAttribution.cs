using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    enum AttackAttribution
    {
        None = -1,
        Beat,
        Cut,
        Thrust,
        Fever,
        Ice,
        Electro,
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
