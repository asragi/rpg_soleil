using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    enum SkillID
    {
        // Light
        PointFlare,
        WarmHeal,
        HeatWave,
        // Shade
        Freeze,
        Bind,
        CoolDown,
        // Magic
        Thunder,
        MagicalHeal,
        Explode,
        // Dark
        Reaper,
        IlFlood,
        LifeSteal,
        // Sound
        Sonicboom,
        Noize,
        Maximizer,
        // Ninja
        Poizon,
        ArmorBreak,
        MirrorShade,
        // Wood
        Relax,
        Fragrance,
        AlomaDrop,
        // Metal
        Alchem,
        PileBunker,
        MetalCoat,
        // Space
        Teleport,
        DimensionKill,
        SeaventhHeaven,
        // Time
        Delay,
        Haste,
        HeavensDrive,
        // Skill
        // -- Sunny
        Headbutt,
        // -- Tella
        Barrage,

        //ActionNameを統合
        NormalAttack,
        NormalMagic,
        ExampleDebuff,

        //Guard
        Guard,
        EndGuard,


        size
    }

    static class SkillDataBase
    {
        static ISkill[] data;
        static SkillDataBase()
        {
            data = new ISkill[(int)SkillID.size];
            SetData();
        }

        public static ISkill Get(SkillID id) => data[(int)id];

        static void SetData()
        {
            // sun
            SetMagic("ポイントフレア", SkillID.PointFlare, MagicCategory.Sun, "敵単体へ熱属性のダメージ．", 6);
            SetMagic("ウォーム", SkillID.WarmHeal, MagicCategory.Sun, "味方単体を回復・攻撃力上昇．", 20);
            SetMagic("ヒートウェイヴ", SkillID.HeatWave, MagicCategory.Sun, "敵全体へ熱属性のダメージ．", 27);
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

            // skill
            SetSkill("ヘッドバット", SkillID.Headbutt, "敵単体に打属性のダメージ．確率で気絶．", 12);
            SetSkill("集中砲火", SkillID.Barrage, "敵単体に突属性のダメージ．", 15);


            SetMagic("はたく", SkillID.NormalAttack, MagicCategory.None, "敵単体に無属性の物理攻撃．", 0);
            SetMagic("ショット", SkillID.NormalMagic, MagicCategory.None, "敵単体に無属性の魔法攻撃．", 0);
            SetSkill("なきごえ", SkillID.ExampleDebuff, "敵単体に攻撃力低下．", 15);

            SetSkill("ガード", SkillID.Guard, "テスト用ガード", 0);
            SetSkill("エンドガード", SkillID.EndGuard, "test", 0);
        }

        static void SetSkill(string name, SkillID id, string desc, int cost, bool onMenu = false, bool onBattle = true)
        {
            data[(int)id] = new SkillData(name, id, desc, cost, onMenu, onBattle);
        }

        static void SetMagic(string name, SkillID id, MagicCategory category, string desc, int cost, bool onMenu = false, bool onBattle = true)
        {
            data[(int)id] = new MagicData(name, id, category, desc, cost, onMenu, onBattle);
        }
    }
}
