using Soleil.Battle;
using Soleil.Item;
using Soleil.Misc;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// キャラクターの初期能力値設定
    /// </summary>
    static class PersonDatabase
    {
        private readonly static Dictionary<CharaName, PersonData> data;

        static PersonDatabase()
        {
            Set(CharaName.Lune, new AbilityScore(72, 156, 3, 5, 30, 8),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 0, 0, 450, 0, 0, 0, 0, 0, 0, 0 },
                new SkillID[] { }
                );
            Set(CharaName.Sunny, new AbilityScore(120, 90, 21, 18, 20, 21),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 100, 0, 200, 0, 0, 0, 0, 0, 0, 0 },
                new SkillID[] { SkillID.Headbutt }
                );
            Set(CharaName.Tella, new AbilityScore(96, 60, 9, 14, 13, 17),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 0, 500, 0, 0, 0, 200, 0, 0, 0, 0 },
                new SkillID[] { }
                );

            void Set(CharaName name, AbilityScore score, ItemID[] eq, int[] mgExp, SkillID[] sk)
            {
                Debug.Assert(eq.Length == 4);
                Debug.Assert(mgExp.Length == 10);
                data.Add(name, new PersonData(score, eq, mgExp, sk));
            }
        }

        public static Person GetPersonData(CharaName name)
        {
            var targetData = data[name];
            var equip = new EquipSet();
            var skill = new SkillHolder(targetData.InitSkill);
            var magic = new MagicLv(targetData.InitMagicExp, skill);
            return new Person(name, targetData.InitScore, skill, magic, equip);
        }
    }
}
