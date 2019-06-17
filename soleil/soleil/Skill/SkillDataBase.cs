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
        // Magic
        Thunder,
        MagicalHeal,
        Explode,

        // Sound
        Sonicboom,

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
            SetMagic("ポイントフレア", SkillID.PointFlare, MagicCategory.Sun, "敵単体へ熱属性のダメージ．");
            SetMagic("サンダーボルト", SkillID.Thunder, MagicCategory.Magic, "敵単体へ電撃属性のダメージ．");
            SetMagic("マジカルヒール", SkillID.MagicalHeal, MagicCategory.Magic, "味方単体を中量回復．");
            SetMagic("エクスプロード", SkillID.Explode, MagicCategory.Magic, "敵全体へ突属性のダメージ．");

            SetMagic("ソニックブーム", SkillID.Sonicboom, MagicCategory.Sound, "敵単体へ斬属性のダメージ．");

            SetSkill("ヘッドバット", SkillID.Headbutt, "敵単体に打属性のダメージ．確率で気絶．");
        }

        static void SetSkill(string name, SkillID id, string desc, bool onMenu = false, bool onBattle = true)
        {
            data[(int)id] = new SkillData(name, id, desc, onMenu, onBattle);
        }

        static void SetMagic(string name, SkillID id, MagicCategory category, string desc, bool onMenu = false, bool onBattle = true)
        {
            data[(int)id] = new MagicData(name, id, category, desc, onMenu, onBattle);
        }
    }
}
