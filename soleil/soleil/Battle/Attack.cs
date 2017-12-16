using System;
using System.Collections.Generic;
using System.Linq;

using static System.Math;
namespace Soleil
{

    public static class Attacks
    {
        static List<Action> actions;
        static List<Func<CharacterStatus, CharacterStatus, float>> attackTable;


        /// <summary>
        /// 補正
        /// [0.8, 1.2)
        /// </summary>
        /// <returns></returns>
        static float Revision()
        {
            return (float)Global.RandomDouble(0.8, 1.2);
        }
        static int Fraction(float x)
        {
            return (int)x;
        }
        static Attacks()
        {
            attackTable = new List<Func<CharacterStatus, CharacterStatus, float>>();
            for (int i = 0; i < (int)ActionName.Size; i++)
                attackTable.Add((a, b) => { return 0; });
            attackTable[(int)ActionName.NormalAttack] = (a, b) => { return Max(a.STR * 4.0f - b.VIT * 2.0f, 5.0f) * Revision(); };
            attackTable[(int)ActionName.ExampleMagic] = (a, b) => { return Max(a.MAG * 4.0f - (b.VIT + b.MAG), 0.0f) * Revision(); };

            actions = new List<Action>();
            for (int i = 0; i < (int)ActionName.Size; i++)
                actions.Add(new Action());
            actions[(int)ActionName.NormalAttack] = new Action(attackTable[(int)ActionName.NormalAttack]);
        }
    }

}