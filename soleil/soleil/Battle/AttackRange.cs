using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Range
{
    //typesame enum
    abstract class AttackRange
    {
        public int SourceIndex;
        public AttackRange(int sourceIndex) => SourceIndex = sourceIndex;
        public abstract bool ContainRange(int index, BattleField bf);
        public AttackRange Clone() =>
            MemberwiseClone() as AttackRange;
    }

    class OneEnemy : AttackRange
    {
        public int TargetIndex;
        public OneEnemy(int sourceIndex, int targetIndex) : base(sourceIndex)
            => TargetIndex = targetIndex;

        static OneEnemy singleton = new OneEnemy(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
    }

    class AllEnemy : AttackRange
    {
        public Side TargetSide;
        public AllEnemy(int sourceIndex, Side targetSide) : base(sourceIndex) => TargetSide = targetSide;

        static AllEnemy singleton = new AllEnemy(-1, Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
    }

    class Me : AttackRange
    {
        public Me(int index) : base(index) { }

        static Me singleton = new Me(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == SourceIndex;
    }

    class Ally : AttackRange
    {
        public int TargetIndex;
        public Ally(int sourceIndex, int targetIndex) : base(sourceIndex)
            => TargetIndex = targetIndex;

        static Ally singleton = new Ally(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
    }

    class AllAlly : AttackRange
    {
        public Side TargetSide;
        public AllAlly(int sourceIndex, Side targetSide) : base(sourceIndex) => TargetSide = targetSide;

        static AllAlly singleton = new AllAlly(-1, Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
    }

    class ForAll : AttackRange
    {
        public ForAll(int sourceIndex) : base(sourceIndex) { }

        static ForAll singleton = new ForAll(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => true;
    }
}
