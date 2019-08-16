using System;
using System.Collections.Generic;
using System.Linq;

using static System.Math;
using Soleil.Skill;
namespace Soleil.Battle
{
    /// <summary>
    /// 行動のデータベース
    /// </summary>
    static class ActionInfo
    {
        static readonly List<Action> actions;
        static readonly List<string> actionString;
        static readonly Dictionary<SkillID, Func<AttackAttribution, Func<CharacterStatus, CharacterStatus, float>>> attackTable;
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, BuffRate>> buffTable;

        static readonly Func<CharacterStatus, CharacterStatus, float, AttackAttribution, float> physicalAttack, magicalAttack;

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
        static ActionInfo()
        {
            physicalAttack = (a, b, force, attr) => { return (a.STR * a.PATK * force * 24) / (a.STR * a.PATK + 1500) * (400 - b.VIT - b.PDEF(attr) * 2) / 400 * Revision(); };
            magicalAttack = (a, b, force, attr) => { return ((a.MAG * a.MATK * force * 24) / (a.MAG * a.MATK + 1500)) * ((400 - (b.VIT + b.MAG * 2) / 3 - b.MDEF(attr) * 2) / 400) * Revision(); };

            attackTable = new Dictionary<SkillID, Func<AttackAttribution, Func<CharacterStatus, CharacterStatus, float>>>();
            attackTable[SkillID.NormalAttack] = (attr) => (a, b) => { return physicalAttack(a, b, 10, attr); };
            attackTable[SkillID.NormalMagic] = (attr) => (a, b) => { return magicalAttack(a, b, 10, attr); };


            buffTable = new Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, BuffRate>>();
            buffTable[SkillID.Guard] = (a, b) =>
            {
                return b.Rates.MultRate(new Dictionary<BuffRateName, float>()
                    { { BuffRateName.VITRate, 2.0f }, { BuffRateName.MAGRate, 2.0f } });
            };
            buffTable[SkillID.EndGuard] = (a, b) =>
            {
                return b.Rates.MultRate(new Dictionary<BuffRateName, float>()
                    { { BuffRateName.VITRate, 0.5f }, { BuffRateName.MAGRate, 0.5f } });
            };
            buffTable[SkillID.ExampleDebuff] = (a, b) =>
            {
                return b.Rates.DecreaseRate(new HashSet<BuffRateName>() { BuffRateName.STRRate });
            };

            actions = new List<Action>();
            for (int i = 0; i < (int)SkillID.size; i++)
                actions.Add(null);
            actionString = new List<String>();
            for (int i = 0; i < (int)SkillID.size; i++)
                actionString.Add("");

            //うまいことSkillDataBaseと統合したい
            actions[(int)SkillID.PointFlare] = new Attack(attackTable[SkillID.NormalAttack](AttackAttribution.Fever), Range.OneEnemy.GetInstance(), mp: 6);
            actionString[(int)SkillID.PointFlare] = "ポイントフレア";

            actions[(int)SkillID.HeatWave] = new Attack(attackTable[SkillID.NormalAttack](AttackAttribution.Fever), Range.AllEnemy.GetInstance(), mp: 27);
            actionString[(int)SkillID.HeatWave] = "ヒートウェイヴ";


            actions[(int)SkillID.Headbutt] = new Attack(attackTable[SkillID.NormalAttack](AttackAttribution.Beat), Range.OneEnemy.GetInstance(), mp: 12);
            actionString[(int)SkillID.Headbutt] = "ヘッドバット";
            //確率で気絶

            actions[(int)SkillID.Barrage] = new Attack(attackTable[SkillID.NormalAttack](AttackAttribution.Thrust), Range.OneEnemy.GetInstance(), mp: 15);
            actionString[(int)SkillID.Barrage] = "集中砲火";

            //samples
            actions[(int)SkillID.NormalAttack] = new Attack(attackTable[SkillID.NormalAttack](AttackAttribution.None), Range.OneEnemy.GetInstance());
            actionString[(int)SkillID.NormalAttack] = "通常攻撃";

            actions[(int)SkillID.NormalMagic] = new Attack(attackTable[SkillID.NormalMagic](AttackAttribution.None), Range.OneEnemy.GetInstance(), mp: 100);
            actionString[(int)SkillID.NormalMagic] = "魔法攻撃";

            actions[(int)SkillID.Guard] = new Buff(buffTable[SkillID.Guard], Range.Me.GetInstance());
            actionString[(int)SkillID.Guard] = "ガード";

            actions[(int)SkillID.EndGuard] = new Buff(buffTable[SkillID.EndGuard], Range.Me.GetInstance());
            actionString[(int)SkillID.EndGuard] = "ガード終了";

            actions[(int)SkillID.ExampleDebuff] = new Buff(buffTable[SkillID.ExampleDebuff], Range.OneEnemy.GetInstance(), mp: 70);
            actionString[(int)SkillID.ExampleDebuff] = "なきごえ";
        }

        public static Action GetAction(SkillID name) => actions[(int)name];
        public static string GetActionName(SkillID name) => actionString[(int)name];
    }

}