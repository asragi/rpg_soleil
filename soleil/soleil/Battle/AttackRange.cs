using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Battle;

namespace Soleil.Range
{
    /// <summary>
    /// (Typename Enum Idiom)
    /// Enum AttackRange { 
    ///      OneEnemy,
    ///      AllEnemy, ...
    /// }
    /// を拡張した記法
    /// 
    /// 派生クラスはGetInstance()でダミーを取得出来る
    /// </summary>
    abstract class AttackRange
    {
        /// <summary>
        /// 行動を行っている者
        /// </summary>
        public int SourceIndex;


        public AttackRange(int sourceIndex) => SourceIndex = sourceIndex;


        /// <summary>
        /// indexが行動対象(Target)に含まれているかを返す
        /// </summary>
        public abstract bool ContainRange(int index, BattleField bf);


        /// <summary>
        /// 行動対象のListを返す
        /// </summary>
        public abstract List<int> Targets(BattleField bf);


        public AttackRange Clone() =>
            MemberwiseClone() as AttackRange;
    }

    /// <summary>
    /// 効果範囲:敵一体
    /// </summary>
    class OneEnemy : AttackRange
    {
        /// <summary>
        /// 行動対象
        /// </summary>
        public int TargetIndex;


        public OneEnemy(int sourceIndex, int targetIndex) : base(sourceIndex)
            => TargetIndex = targetIndex;

        static OneEnemy singleton = new OneEnemy(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
        public override List<int> Targets(BattleField bf) => new List<int> { TargetIndex };
    }

    /// <summary>
    /// 効果範囲:敵全員
    /// </summary>
    class AllEnemy : AttackRange
    {
        public Side TargetSide;
        public AllEnemy(int sourceIndex, Side targetSide) : base(sourceIndex) => TargetSide = targetSide;

        static AllEnemy singleton = new AllEnemy(-1, Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
        public override List<int> Targets(BattleField bf) => bf.SameSideIndexes(TargetSide);
    }

    /// <summary>
    /// 効果範囲:自身
    /// </summary>
    class Me : AttackRange
    {
        public Me(int index) : base(index) { }

        static Me singleton = new Me(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == SourceIndex;
        public override List<int> Targets(BattleField bf) => new List<int> { SourceIndex };
    }

    /// <summary>
    /// 効果範囲:味方一体
    /// </summary>
    class Ally : AttackRange
    {
        public int TargetIndex;
        public Ally(int sourceIndex, int targetIndex) : base(sourceIndex)
            => TargetIndex = targetIndex;

        static Ally singleton = new Ally(-1, -1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index == TargetIndex;
        public override List<int> Targets(BattleField bf) => new List<int> { TargetIndex };
    }

    /// <summary>
    /// 効果範囲:味方全員(自分含む)
    /// </summary>
    class AllAlly : AttackRange
    {
        public Side TargetSide;
        public AllAlly(int sourceIndex, Side targetSide) : base(sourceIndex) => TargetSide = targetSide;

        static AllAlly singleton = new AllAlly(-1, Side.Size);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => bf.SameSideIndexes(TargetSide).Contains(index);
        public override List<int> Targets(BattleField bf) => bf.SameSideIndexes(TargetSide);
    }

    /// <summary>
    /// 効果範囲:敵味方全員(自分含む)
    /// </summary>
    class ForAll : AttackRange
    {
        public ForAll(int sourceIndex) : base(sourceIndex) { }

        static ForAll singleton = new ForAll(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => true;
        public override List<int> Targets(BattleField bf) => bf.AliveIndexes();
    }

    /// <summary>
    /// 効果範囲:敵味方全員(自分以外)
    /// </summary>
    class ForOthers : AttackRange
    {
        public ForOthers(int sourceIndex) : base(sourceIndex) { }

        static ForOthers singleton = new ForOthers(-1);
        public static AttackRange GetInstance() => singleton;

        public override bool ContainRange(int index, BattleField bf) => index != SourceIndex;
        public override List<int> Targets(BattleField bf)
        {
            var tmp = bf.AliveIndexes();
            tmp.Remove(SourceIndex);
            return tmp;
        }
    }
}
