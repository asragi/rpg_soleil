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
        // Shade
        Freeze,
        // Magic
        Thunder,
        MagicalHeal,
        Explode,
        // Dark
        Reaper,
        // Sound
        Sonicboom,
        // Ninja
        Poizon,
        // Wood
        Relax,
        // Metal
        Alchem,
        // Space
        Teleport,
        // Time
        Delay,
        // Skill
        Headbutt,

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
            // shade
            SetMagic("フリーズ", SkillID.Freeze, MagicCategory.Shade, "敵単体へ冷気属性のダメージ．", 5);
            // magic
            SetMagic("サンダーボルト", SkillID.Thunder, MagicCategory.Magic, "敵単体へ電撃属性のダメージ．", 9);
            SetMagic("マジカルヒール", SkillID.MagicalHeal, MagicCategory.Magic, "味方単体を中量回復．", 45, true);
            SetMagic("エクスプロード", SkillID.Explode, MagicCategory.Magic, "敵全体へ突属性のダメージ．", 73);
            // dark
            SetMagic("リーパー", SkillID.Reaper, MagicCategory.Dark, "敵単体へ斬属性のダメージ．確率で即死．", 13);
            // sound
            SetMagic("ソニックブーム", SkillID.Sonicboom, MagicCategory.Sound, "敵単体へ斬属性のダメージ．", 7);
            // ninja
            SetMagic("ポイズンミスト", SkillID.Poizon, MagicCategory.Shinobi, "敵全体に確率で毒付与．", 12);
            // wood
            SetMagic("リラクシード", SkillID.Relax, MagicCategory.Wood, "味方単体に毎ターン微量回復効果を付与", 14);
            // metal
            SetMagic("アルケム", SkillID.Alchem, MagicCategory.Metal, "一部のアイテムを変換する．", 8);
            // space
            SetMagic("テレポート", SkillID.Teleport, MagicCategory.Space, "ワールドマップで時間経過なく移動できる．", 20, onBattle: false);
            // time
            SetMagic("ディレイ", SkillID.Delay, MagicCategory.Time, "敵単体のSPDを低下．", 12);

            // skill
            SetSkill("ヘッドバット", SkillID.Headbutt, "敵単体に打属性のダメージ．確率で気絶．", 12);
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
