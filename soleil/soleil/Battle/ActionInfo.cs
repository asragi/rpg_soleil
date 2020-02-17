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
        /// <summary>
        /// Actionのデータ 本体
        /// </summary>
        static readonly List<Action> actions;


        /// <summary>
        /// actionsを生成するのを楽にするためのテーブル
        /// </summary>
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, float>> attackTable;
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, BuffRate>> buffTable;
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, Tuple<float, float>>> healTable;

        /// <summary>
        /// 典型的な攻撃の威力計算関数
        /// </summary>
        static readonly Func<CharacterStatus, CharacterStatus, float, AttackAttribution, float> physicalAttack, magicalAttack;
        static readonly Func<CharacterStatus, CharacterStatus, float, Tuple<float, float>> healFunc;

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


        /// <summary>
        /// SkillDataBaseから取得した情報を元にAttackを生成する補助関数
        /// </summary>
        static void SetAttack(SkillID id, Range.AttackRange aRange, EffectAnimationID eaID)
        {
            actions[(int)id] = new Attack(attackTable[id], aRange, eaID, mp: SkillDataBase.Get(id).Cost);
        }
        static void SetBuff(SkillID id, Range.AttackRange aRange)
        {
            actions[(int)id] = new Buff(buffTable[id], aRange, mp: SkillDataBase.Get(id).Cost);
        }
        static void SetHeal(SkillID id, Range.AttackRange aRange)
        {
            actions[(int)id] = new Heal(healTable[id], aRange, mp: SkillDataBase.Get(id).Cost);
        }

        static ActionInfo()
        {
            physicalAttack = (a, b, force, attr) => { return (a.STR * a.PATK * force * 24) / (a.STR * a.PATK + 1500) * (400 - b.VIT - b.PDEF(attr) * 2) / 400 * Revision(); };
            magicalAttack = (a, b, force, attr) => { return ((a.MAG * a.MATK * force * 24) / (a.MAG * a.MATK + 1500)) * ((400 - (b.VIT + b.MAG * 2) / 3 - b.MDEF(attr) * 2) / 400) * Revision(); };
            healFunc = (a, b, force) => Tuple.Create<float, float>((float)(b.AScore.HPMAX * (force / 100.0) * (a.MATK + a.MAG + b.VIT + 3.0) / 300.0) * Revision(), 0);


            #region Attack Table
            attackTable = new Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, float>>();
            attackTable[SkillID.PointFlare] = (a, b) => { return magicalAttack(a, b, 19, AttackAttribution.Fever); };
            attackTable[SkillID.HeatWave] = (a, b) => { return magicalAttack(a, b, 40, AttackAttribution.Fever); };
            attackTable[SkillID.Freeze] = (a, b) => { return magicalAttack(a, b, 20, AttackAttribution.Ice); };
            attackTable[SkillID.Thunder] = (a, b) => { return magicalAttack(a, b, 22, AttackAttribution.Electro); };
            attackTable[SkillID.Explode] = (a, b) => { return magicalAttack(a, b, 44, AttackAttribution.Thrust); };
            attackTable[SkillID.Sonicboom] = (a, b) => { return magicalAttack(a, b, 39, AttackAttribution.Cut); };
            attackTable[SkillID.PileBunker] = (a, b) => { return magicalAttack(a, b, 41, AttackAttribution.Thrust); };
            attackTable[SkillID.DimensionKill] = (a, b) => { return magicalAttack(a, b, 88, AttackAttribution.None); };

            attackTable[SkillID.Headbutt] = (a, b) => { return physicalAttack(a, b, 36, AttackAttribution.None); };
            attackTable[SkillID.Barrage] = (a, b) => { return physicalAttack(a, b, 44, AttackAttribution.None); };
            attackTable[SkillID.NormalAttack] = (a, b) => { return physicalAttack(a, b, 15, AttackAttribution.None); };
            attackTable[SkillID.NormalMagic] = (a, b) => { return magicalAttack(a, b, 15, AttackAttribution.None); };
            #endregion

            #region Buff Table
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
            buffTable[SkillID.WarmHeal] = (a, b) => b.Rates.IncreaseRate(new HashSet<BuffRateName>() { BuffRateName.STRRate });
            buffTable[SkillID.Maximizer] = (a, b) => b.Rates.IncreaseRate(new HashSet<BuffRateName>() { BuffRateName.STRRate, BuffRateName.VITRate });
            buffTable[SkillID.MetalCoat] = (a, b) => b.Rates.IncreaseRate(new HashSet<BuffRateName>() { BuffRateName.VITRate });
            buffTable[SkillID.SeaventhHeaven] = (a, b) => b.Rates.IncreaseRate(new HashSet<BuffRateName>(Enum.GetValues(typeof(BuffRateName)).Cast<BuffRateName>())); //全部の能力強化
            buffTable[SkillID.Delay] = (a, b) => b.Rates.DecreaseRate(new HashSet<BuffRateName> { BuffRateName.SPDRate });
            buffTable[SkillID.Haste] = (a, b) => b.Rates.IncreaseRate(new HashSet<BuffRateName> { BuffRateName.SPDRate });
            #endregion

            #region Heal Table
            healTable = new Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, Tuple<float, float>>>();
            healTable[SkillID.WarmHeal] = (a, _) => healFunc(a, _, 30);
            healTable[SkillID.MagicalHeal] = (a, _) => healFunc(a, _, 60);
            healTable[SkillID.Fragrance] = (a, _) => healFunc(a, _, 20);
            healTable[SkillID.AlomaDrop] = (a, _) => healFunc(a, _, 60);
            #endregion


            actions = new List<Action>();
            for (int i = 0; i < (int)SkillID.size; i++)
                actions.Add(new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), EffectAnimationID.Explode, mp: 0)); //ダミーをつめる
                                                                                                                                            //actions.Add(null);

            // sun
            SetAttack(SkillID.PointFlare, Range.OneEnemy.GetInstance(), EffectAnimationID.PointFlare);

            actions[(int)SkillID.WarmHeal] = new ActionSeq(new List<Action> {
                new Heal(healTable[SkillID.WarmHeal], Range.Ally.GetInstance()),
                new Buff(buffTable[SkillID.WarmHeal], Range.Ally.GetInstance()),
            }, Range.Ally.GetInstance(), mp: 20);

            SetAttack(SkillID.HeatWave, Range.AllEnemy.GetInstance(), EffectAnimationID.Explode);


            // shade
            SetAttack(SkillID.Freeze, Range.OneEnemy.GetInstance(), EffectAnimationID.Explode);
            //SetMagic("バインド", SkillID.Bind, MagicCategory.Shade, "敵単体に確率でマヒ付与．", 5);
            //SetMagic("クールダウン", SkillID.CoolDown, MagicCategory.Shade, "敵単体へ冷気属性ダメージ．確率で攻撃力低下．", 16);


            // magic
            SetAttack(SkillID.Thunder, Range.OneEnemy.GetInstance(), EffectAnimationID.Thunder);
            SetHeal(SkillID.MagicalHeal, Range.Ally.GetInstance());
            SetAttack(SkillID.Explode, Range.AllEnemy.GetInstance(), EffectAnimationID.Explode);


            // dark
            //SetMagic("リーパー", SkillID.Reaper, MagicCategory.Dark, "敵単体へ斬属性のダメージ．確率で即死．", 13);
            //SetMagic("イルフラッド", SkillID.IlFlood, MagicCategory.Dark, "敵全体へランダムな状態異常付与．", 18);
            //SetMagic("ライフスティール", SkillID.LifeSteal, MagicCategory.Dark, "敵単体へ無属性のダメージ．吸収効果．", 13);


            // sound
            SetAttack(SkillID.Sonicboom, Range.OneEnemy.GetInstance(), EffectAnimationID.Explode);
            //SetMagic("スリップノイズ", SkillID.Noize, MagicCategory.Sound, "敵全体に確率でスタン付与．", 8);
            SetBuff(SkillID.Maximizer, Range.Ally.GetInstance());


            // ninja
            //SetMagic("ポイズンミスト", SkillID.Poizon, MagicCategory.Shinobi, "敵全体に確率で毒付与．", 11);
            //SetMagic("アーマーブレイク", SkillID.ArmorBreak, MagicCategory.Shinobi, "敵単体へ打属性ダメージ．確率で防御低下．", 15);
            //SetMagic("ミラーシェイド", SkillID.MirrorShade, MagicCategory.Shinobi, "1度だけダメージを無効化．", 60);


            // wood
            //SetMagic("リラクシード", SkillID.Relax, MagicCategory.Wood, "味方単体に毎ターン微量回復効果を付与．", 14);
            SetHeal(SkillID.Fragrance, Range.AllAlly.GetInstance());
            SetHeal(SkillID.AlomaDrop, Range.Ally.GetInstance());


            // metal
            //SetMagic("アルケム", SkillID.Alchem, MagicCategory.Metal, "一部のアイテムを変換する．", 8);
            SetAttack(SkillID.PileBunker, Range.OneEnemy.GetInstance(), EffectAnimationID.Explode);
            SetBuff(SkillID.MetalCoat, Range.AllAlly.GetInstance());


            // space
            //SetMagic("テレポート", SkillID.Teleport, MagicCategory.Space, "ワールドマップで時間経過なく移動できる．", 20, onBattle: false);
            SetAttack(SkillID.DimensionKill, Range.AllEnemy.GetInstance(), EffectAnimationID.Explode);
            SetBuff(SkillID.SeaventhHeaven, Range.AllAlly.GetInstance());


            // time
            SetBuff(SkillID.Delay, Range.OneEnemy.GetInstance());
            SetBuff(SkillID.Haste, Range.Ally.GetInstance());
            //SetMagic("ヘヴンズドライヴ", SkillID.HeavensDrive, MagicCategory.Time, "魔力に応じた連続行動．", 99);


            // skill
            actions[(int)SkillID.Headbutt] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), EffectAnimationID.Explode, mp: 12);
            //確率で気絶

            actions[(int)SkillID.Barrage] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), EffectAnimationID.Explode, mp: 15);

            //samples
            actions[(int)SkillID.NormalAttack] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), EffectAnimationID.Blow);
            actions[(int)SkillID.NormalMagic] = new Attack(attackTable[SkillID.NormalMagic], Range.OneEnemy.GetInstance(), EffectAnimationID.Explode, mp: 100);
            actions[(int)SkillID.Guard] = new Buff(buffTable[SkillID.Guard], Range.Me.GetInstance());
            actions[(int)SkillID.EndGuard] = new Buff(buffTable[SkillID.EndGuard], Range.Me.GetInstance());
            actions[(int)SkillID.ExampleDebuff] = new Buff(buffTable[SkillID.ExampleDebuff], Range.OneEnemy.GetInstance(), mp: 70);
            actions[(int)SkillID.Escape] = new Escape((a, b) => { return 0.2f; });
        }

        public static Action GetAction(SkillID name) => actions[(int)name];
        public static string GetActionName(SkillID name) => SkillDataBase.Get(name).Name; //actionString[(int)name];
    }

}