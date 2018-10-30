﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Range
{
    //typesame enum
    abstract class AttackRange
    {
        public abstract bool ContainRange(int index, BattleField bf);
    }

    class OneEnemy : AttackRange
    {
        public int SourceIndex;
        public int TargetIndex;
        public OneEnemy(int sourceIndex, int targetIndex)
            => (SourceIndex, TargetIndex) = (sourceIndex, targetIndex);

        static OneEnemy singleton = new OneEnemy(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
    }

    class AllEnemy : AttackRange
    {
        public Side TargetSide;
        public AllEnemy(Side targetSide) => TargetSide = targetSide;

        static AllEnemy singleton = new AllEnemy(Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
    }

    class Me : AttackRange
    {
        public int Index;
        public Me(int index) => Index = index;

        static Me singleton = new Me(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == Index;
    }

    class Ally : AttackRange
    {
        public int SourceIndex;
        public int TargetIndex;
        public Ally(int sourceIndex, int targetIndex)
            => (SourceIndex, TargetIndex) = (sourceIndex, targetIndex);

        static Ally singleton = new Ally(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
    }

    class AllAlly : AttackRange
    {
        public Side TargetSide;
        public AllAlly(Side targetSide) => TargetSide = targetSide;

        static AllAlly singleton = new AllAlly(Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
    }

    class ForAll : AttackRange
    {
        public int SourceIndex;
        public ForAll(int sourceIndex) => SourceIndex = sourceIndex;

        static ForAll singleton = new ForAll(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => true;
    }
}