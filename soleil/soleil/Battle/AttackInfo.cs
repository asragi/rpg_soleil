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

        static Func<CharacterStatus, CharacterStatus, float, float> physicalAttack, magicalAttack;

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
            physicalAttack = (a, b, force) => { return (a.STR * a.PATK * force * 24) / (a.STR * a.PATK + 1500) * (400 - b.VIT - b.PDEF * 2) / 400 * Revision(); };
            magicalAttack = (a, b, force) => { return ((a.MAG * a.MATK * force * 24) / (a.MAG * a.MATK + 1500))*((400 - (b.VIT + b.MAG * 2)/ 3 - b.MDEF * 2) / 400) * Revision(); };

            attackTable = new Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, float>>();
            attackTable[ActionName.NormalAttack] = (a, b) => { return physicalAttack(a, b, 10); };
            attackTable[ActionName.ExampleMagic] = (a, b) => { return magicalAttack(a, b, 10); };


            buffTable = new Dictionary<ActionName, Func<CharacterStatus, CharacterStatus, BuffRate>>();
            buffTable[ActionName.Guard] = (a, b) => {
                return b.Rates.MultRate(new Dictionary<BuffRateName, float>()
                    { { BuffRateName.VITRate, 2.0f }, { BuffRateName.MAGRate, 2.0f } });
                };
            buffTable[ActionName.EndGuard] = (a, b) => {
                return b.Rates.MultRate(new Dictionary<BuffRateName, float>()
                    { { BuffRateName.VITRate, 0.5f }, { BuffRateName.MAGRate, 0.5f } });
            };
            buffTable[ActionName.ExampleDebuff] = (a, b) => {
                return b.Rates.DecreaseRate(new HashSet<BuffRateName>() { BuffRateName.STRRate });
            };

            actions = new List<Action>();
            for (int i = 0; i < (int)ActionName.Size; i++)
                actions.Add(null);
            actions[(int)ActionName.NormalAttack] = new Attack(attackTable[ActionName.NormalAttack], Range.OneEnemy.GetInstance());
            actions[(int)ActionName.ExampleMagic] = new Attack(attackTable[ActionName.ExampleMagic], Range.OneEnemy.GetInstance());

            actions[(int)ActionName.Guard] = new Buff(buffTable[ActionName.Guard], Range.Me.GetInstance());
            actions[(int)ActionName.EndGuard] = new Buff(buffTable[ActionName.EndGuard], Range.Me.GetInstance());
            actions[(int)ActionName.ExampleDebuff] = new Buff(buffTable[ActionName.ExampleDebuff], Range.OneEnemy.GetInstance());
        }

        public static Action GetAction(ActionName name) => actions[(int)name];
    }

}