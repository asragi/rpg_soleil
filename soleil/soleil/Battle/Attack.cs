using System;
using System.Collections.Generic;
using System.Linq;

using static System.Math;
namespace Soleil
{
    enum AttackName
    {
        NormalAttack,
        ExampleMagic,

        Size,
    }

    public static class Attacks
    {
        static List<Func<AbilityScore, AbilityScore, int>> attackTable;

        /// <summary>
        /// 補正
        /// [0.8, 1.2)
        /// </summary>
        /// <returns></returns>
        static float Revision()
        {
            return (float)Global.RandomDouble(0.8, 1.2);
        }

        static Attacks()
        {
            attackTable = new List<Func<AbilityScore, AbilityScore, int>>();
            attackTable[(int)AttackName.NormalAttack] = (a, b) => { return (int)(Max(a.STR * 4 - b.VIT * 2, 5) * Revision()); };
            attackTable[(int)AttackName.ExampleMagic] = (a, b) => { return (int)(Max(a.MAG * 4 - (b.VIT + b.MAG), 0) * Revision()); };
        }
    }

}