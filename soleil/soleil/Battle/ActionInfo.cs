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
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, float>> attackTable;
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, BuffRate>> buffTable;
        static readonly Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, Tuple<float, float>>> healTable;

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

            attackTable = new Dictionary<SkillID, Func<CharacterStatus, CharacterStatus, float>>();
            attackTable[SkillID.PointFlare] = (a, b) => { return magicalAttack(a, b, 10, AttackAttribution.Fever); };
            attackTable[SkillID.HeatWave] = (a, b) => { return magicalAttack(a, b, 10, AttackAttribution.Fever); };
            attackTable[SkillID.Headbutt] = (a, b) => { return physicalAttack(a, b, 10, AttackAttribution.None); };
            attackTable[SkillID.Barrage] = (a, b) => { return physicalAttack(a, b, 10, AttackAttribution.None); };
            attackTable[SkillID.NormalAttack] = (a, b) => { return physicalAttack(a, b, 10, AttackAttribution.None); };
            attackTable[SkillID.NormalMagic] = (a, b) => { return magicalAttack(a, b, 10, AttackAttribution.None); };


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
                actions.Add(new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), mp: 0)); //ダミーをつめる
            //actions.Add(null);

            //うまいことSkillDataBaseと統合したい
            actions[(int)SkillID.PointFlare] = new Attack(attackTable[SkillID.PointFlare], Range.OneEnemy.GetInstance(), mp: 6);

            //actions[(int)SkillID.WarmHeal] = new Attack(attackTable[SkillID.WarmHeal], Range.OneEnemy.GetInstance(), mp: 6);
            //actionString[(int)SkillID.WarmHeal] = "ウォーム";
            //SetMagic("ウォーム", SkillID.WarmHeal, MagicCategory.Sun, "味方単体を回復・攻撃力上昇．", 20);

            actions[(int)SkillID.HeatWave] = new Attack(attackTable[SkillID.HeatWave], Range.AllEnemy.GetInstance(), mp: 27);
            /*
            // shade
            SetMagic("フリーズ", SkillID.Freeze, MagicCategory.Shade, "敵単体へ冷気属性のダメージ．", 5);
            SetMagic("バインド", SkillID.Bind, MagicCategory.Shade, "敵単体に確率でマヒ付与．", 5);
            SetMagic("クールダウン", SkillID.CoolDown, MagicCategory.Shade, "敵単体へ冷気属性ダメージ．確率で攻撃力低下．", 16);
            // magic
            SetMagic("サンダーボルト", SkillID.Thunder, MagicCategory.Magic, "敵単体へ電撃属性のダメージ．", 9);
            SetMagic("マジカルヒール", SkillID.MagicalHeal, MagicCategory.Magic, "味方単体を中量回復．", 45, true);
            SetMagic("エクスプロード", SkillID.Explode, MagicCategory.Magic, "敵全体へ突属性のダメージ．", 73);
            // dark
            SetMagic("リーパー", SkillID.Reaper, MagicCategory.Dark, "敵単体へ斬属性のダメージ．確率で即死．", 13);
            SetMagic("イルフラッド", SkillID.IlFlood, MagicCategory.Dark, "敵全体へランダムな状態異常付与．", 18);
            SetMagic("ライフスティール", SkillID.LifeSteal, MagicCategory.Dark, "敵単体へ無属性のダメージ．吸収効果．", 13);
            // sound
            SetMagic("ソニックブーム", SkillID.Sonicboom, MagicCategory.Sound, "敵単体へ斬属性のダメージ．", 8);
            SetMagic("スリップノイズ", SkillID.Noize, MagicCategory.Sound, "敵全体に確率でスタン付与．", 8);
            SetMagic("マキシマイザ", SkillID.Maximizer, MagicCategory.Sound, "味方単体の攻撃力・防御力上昇．", 46);
            // ninja
            SetMagic("ポイズンミスト", SkillID.Poizon, MagicCategory.Shinobi, "敵全体に確率で毒付与．", 11);
            SetMagic("アーマーブレイク", SkillID.ArmorBreak, MagicCategory.Shinobi, "敵単体へ打属性ダメージ．確率で防御低下．", 15);
            SetMagic("ミラーシェイド", SkillID.MirrorShade, MagicCategory.Shinobi, "1度だけダメージを無効化．", 60);
            // wood
            SetMagic("リラクシード", SkillID.Relax, MagicCategory.Wood, "味方単体に毎ターン微量回復効果を付与．", 14);
            SetMagic("フレグランス", SkillID.Fragrance, MagicCategory.Wood, "味方全体を回復．", 76);
            SetMagic("アロマドロップ", SkillID.AlomaDrop, MagicCategory.Wood, "味方単体を全回復．", 55);
            // metal
            SetMagic("アルケム", SkillID.Alchem, MagicCategory.Metal, "一部のアイテムを変換する．", 8);
            SetMagic("パイルバンカー", SkillID.PileBunker, MagicCategory.Metal, "敵単体に突属性ダメージ．", 15);
            SetMagic("メタルコート", SkillID.MetalCoat, MagicCategory.Metal, "味方全体の防御力上昇．", 25);
            // space
            SetMagic("テレポート", SkillID.Teleport, MagicCategory.Space, "ワールドマップで時間経過なく移動できる．", 20, onBattle: false);
            SetMagic("ディメンジョンキル", SkillID.DimensionKill, MagicCategory.Space, "敵全体に無属性ダメージ．", 88);
            SetMagic("セヴンスヘヴン", SkillID.SeaventhHeaven, MagicCategory.Space, "味方全体の全能力上昇．", 82);
            // time
            SetMagic("ディレイ", SkillID.Delay, MagicCategory.Time, "敵単体の行動速度低下．", 12);
            SetMagic("アクセラレート", SkillID.Haste, MagicCategory.Time, "味方単体の行動速度上昇．", 40);
            SetMagic("ヘヴンズドライヴ", SkillID.HeavensDrive, MagicCategory.Time, "魔力に応じた連続行動．", 99);
            */

            actions[(int)SkillID.Headbutt] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), mp: 12);
            //確率で気絶

            actions[(int)SkillID.Barrage] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance(), mp: 15);

            //samples
            actions[(int)SkillID.NormalAttack] = new Attack(attackTable[SkillID.NormalAttack], Range.OneEnemy.GetInstance());

            actions[(int)SkillID.NormalMagic] = new Attack(attackTable[SkillID.NormalMagic], Range.OneEnemy.GetInstance(), mp: 100);

            actions[(int)SkillID.Guard] = new Buff(buffTable[SkillID.Guard], Range.Me.GetInstance());

            actions[(int)SkillID.EndGuard] = new Buff(buffTable[SkillID.EndGuard], Range.Me.GetInstance());

            actions[(int)SkillID.ExampleDebuff] = new Buff(buffTable[SkillID.ExampleDebuff], Range.OneEnemy.GetInstance(), mp: 70);
        }

        public static Action GetAction(SkillID name) => actions[(int)name];
        public static string GetActionName(SkillID name) => SkillDataBase.Get(name).Name; //actionString[(int)name];
    }

}