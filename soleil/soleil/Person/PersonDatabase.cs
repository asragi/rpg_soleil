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
            data = new Dictionary<CharaName, PersonData>();
            Set(CharaName.Lune,
                new[] { GrowthType.Normal, GrowthType.Pre, GrowthType.Late, GrowthType.Normal, GrowthType.Pre, GrowthType.Normal },
                new AbilityScore(72, 156, 3, 5, 30, 8), new AbilityScore(666, 999, 30, 30, 99, 60),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 0, 0, 100, 0, 0, 0, 0, 0, 0, 0 },
                new SkillID[] { }
                );
            Set(CharaName.Sunny,
                new[] { GrowthType.Pre, GrowthType.Normal, GrowthType.Pre, GrowthType.Pre, GrowthType.Normal, GrowthType.Normal },
                new AbilityScore(120, 90, 21, 18, 20, 21), new AbilityScore(990, 950, 99, 99, 95, 99),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 10, 0, 50, 0, 0, 0, 0, 0, 0, 0 },
                new SkillID[] { SkillID.Headbutt }
                );
            Set(CharaName.Tella,
                new[] { GrowthType.Normal, GrowthType.Normal, GrowthType.Normal, GrowthType.Normal, GrowthType.Normal, GrowthType.Normal },
                new AbilityScore(96, 60, 9, 14, 13, 17), new AbilityScore(720, 770, 65, 80, 70, 80),
                new[] { ItemID.OldWand, ItemID.Uniform, ItemID.BeadsWork, ItemID.BeadsWork },
                new[] { 0, 500, 0, 0, 0, 0, 0, 200, 0, 0 },
                new SkillID[] { SkillID.Barrage }
                );

            void Set(CharaName name, GrowthType[] growth,
                AbilityScore init, AbilityScore last,
                ItemID[] eq, int[] mgExp, SkillID[] sk)
            {
                Debug.Assert(eq.Length == 4);
                Debug.Assert(mgExp.Length == 10);
                data.Add(name, new PersonData(growth, init, last, eq, mgExp, sk));
            }
        }

        public static PersonData Get(CharaName name) => data[name];
    }
}
