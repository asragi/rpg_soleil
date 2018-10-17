using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    //typesame enum
    abstract class AttackRange
    {
    }

    class OneEnemy : AttackRange
    {
        public int SourceIndex;
        public int TargetIndex;
        public OneEnemy(int sourceIndex, int targetIndex)
            => (SourceIndex, TargetIndex) = (sourceIndex, targetIndex);
        public OneEnemy()
            => (SourceIndex, TargetIndex) = (-1, -1);
    }

    class AllEnemy : AttackRange
    {
        public Side TargetSide;
        public AllEnemy(Side targetSide) => TargetSide = targetSide;
        public AllEnemy() => TargetSide = Side.Size;
    }

    class Me : AttackRange
    {
        public int Index;
        public Me(int index) => Index = index;
        public Me() => Index = -1;
    }

    class Ally : AttackRange
    {
        public int SourceIndex;
        public int TargetIndex;
        public Ally(int sourceIndex, int targetIndex)
            => (SourceIndex, TargetIndex) = (sourceIndex, targetIndex);
        public Ally()
            => (SourceIndex, TargetIndex) = (-1, -1);
    }

    class AllAlly : AttackRange
    {
        public Side TargetSide;
        public AllAlly(Side targetSide) => TargetSide = targetSide;
        public AllAlly() => TargetSide = Side.Size;
    }

    class ForAll : AttackRange
    {
        public int SourceIndex;
        public ForAll(int sourceIndex) => SourceIndex = sourceIndex;
        public ForAll() => SourceIndex = -1;
    }
}
