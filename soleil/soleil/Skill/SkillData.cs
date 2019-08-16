using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Skill
{
    enum MagicCategory
    {
        None = -1,
        Sun,
        Shade,
        Magic,
        Dark,
        Wood,
        Metal,
        Sound,
        Shinobi,
        Space,
        Time,
        size,
    }

    enum AttackType
    {
        Physical,
        Magical,
        Other,
    }

    interface ISkill
    {
        SkillID ID { get; }
        AttackType AttackType { get; }
        bool OnMenu { get; }
        bool OnBattle { get; }
        string Name { get; }
        string Description { get; }
        int Cost { get; }
        Range.AttackRange TargetRange { get; }
    }

    struct MagicData : ISkill
    {
        public SkillID ID { get; }
        public AttackType AttackType { get; }
        public MagicCategory Category { get; }
        public bool OnMenu { get; }
        public bool OnBattle { get; }
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }
        public Range.AttackRange TargetRange { get; }
        public MagicData(
            string name, SkillID id, MagicCategory category, string desc, int cost,
            Range.AttackRange target,bool onMenu = false, bool onBattle = true)
        {
            (ID, Category, OnMenu, OnBattle, Name, Description, Cost, TargetRange) =
                (id, category, onMenu, onBattle, name, desc, cost, target);
            AttackType = AttackType.Magical;
        }
    }

    struct SkillData : ISkill
    {
        public SkillID ID { get; }
        public AttackType AttackType { get; }
        public bool OnMenu { get; }
        public bool OnBattle { get; }
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }
        public Range.AttackRange TargetRange { get; }
        public SkillData(string name, SkillID id, string desc, int cost, Range.AttackRange range, bool onMenu = false, bool onBattle = true, AttackType aType = AttackType.Physical)
        {
            (ID, AttackType, OnMenu, OnBattle, Name, Description, Cost,TargetRange) =
                (id, aType, onMenu, onBattle, name, desc, cost,range);
        }
    }
}
