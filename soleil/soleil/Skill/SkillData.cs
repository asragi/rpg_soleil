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

        public MagicData(
            string name, SkillID id, MagicCategory category, string desc, int cost,
            bool onMenu = false, bool onBattle = true)
        {
            (ID, Category, OnMenu, OnBattle, Name, Description, Cost) =
                (id, category, onMenu, onBattle, name, desc, cost);
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

        public SkillData(string name, SkillID id, string desc, int cost, bool onMenu = false, bool onBattle = true, AttackType aType = AttackType.Physical)
        {
            (ID, AttackType, OnMenu, OnBattle, Name, Description, Cost) =
                (id, aType, onMenu, onBattle, name, desc, cost);
        }
    }
}
