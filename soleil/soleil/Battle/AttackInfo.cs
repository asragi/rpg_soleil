using System;
using System.Collections.Generic;
using System.Linq;

using static System.Math;
namespace Soleil
{

    static class AttackInfo
    {
        static List<Action> actions;
        static Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, float>> attackTable;
        static Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, BuffRate>> buffTable;


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
        static AttackInfo()
        {
            attackTable = new Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, float>>();
            attackTable[ActionName.NormalAttack] = (a, b) => { return Max(a.STR * 4.0f - b.VIT * 2.0f, 5.0f) * Revision(); };
            attackTable[ActionName.ExampleMagic] = (a, b) => { return Max(a.MAG * 4.0f - (b.VIT + b.MAG), 0.0f) * Revision(); };


            buffTable = new Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, BuffRate>>();
            buffTable[ActionName.Guard] = (a, b) => { return b.Rates.MultRate(VITrate: 2.0f, MAGrate: 2.0f); };
            buffTable[ActionName.EndGuard] = (a, b) => { return b.Rates.MultRate(VITrate: 0.5f, MAGrate: 0.5f); };
            buffTable[ActionName.ExampleDebuff] = (a, b) => { return b.Rates.MultRate(STRrate: 0.5f); };

            actions = new List<Action>();
            for (int i = 0; i < (int)ActionName.Size; i++)
                actions.Add(null);
            actions[(int)ActionName.NormalAttack] = new AttackForOne(attackTable[ActionName.NormalAttack]);
            actions[(int)ActionName.ExampleMagic] = new AttackForOne(attackTable[ActionName.ExampleMagic]);

            actions[(int)ActionName.Guard] = new BuffMe(buffTable[ActionName.Guard]);
            actions[(int)ActionName.EndGuard] = new BuffMe(buffTable[ActionName.EndGuard]);
            actions[(int)ActionName.ExampleDebuff] = new BuffForOne(buffTable[ActionName.ExampleDebuff]);
        }

        public static Action GetAction(ActionName name) => actions[(int)name];
    }

}