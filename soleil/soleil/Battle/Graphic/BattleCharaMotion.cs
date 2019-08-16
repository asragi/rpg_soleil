using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    enum BattleCharaMotionType : int
    {
        Stand,
        Chant,
        Magic,

        Size,
    }

    class BattleCharaMotion
    {
        protected BattleCharaMotionType BCMotionType;
        public BattleCharaMotion(BattleCharaMotionType bcMotionType) => BCMotionType = bcMotionType;

        public virtual Tuple<BattleCharaAnimationType, BattleCharaMotionType> Update(BattleCharaAnimation bcAnim)
            => Tuple.Create(BattleCharaAnimationType.Stand, BCMotionType);
    }

    class BattleCharaMotionWithTime : BattleCharaMotion
    {
        int timer = 0;
        BattleCharaAnimationType bcaType;
        BattleCharaMotionType nextMotionType;
        public BattleCharaMotionWithTime(BattleCharaMotionType bcMotionType, BattleCharaAnimationType bcaType_, int time, BattleCharaMotionType nextMotionType_ = BattleCharaMotionType.Stand)
            : base(bcMotionType)
            => (bcaType, timer, nextMotionType) = (bcaType_, time, nextMotionType_);

        public override Tuple<BattleCharaAnimationType, BattleCharaMotionType> Update(BattleCharaAnimation bcAnim)
        {
            if (timer > 1)
                timer--;
            else if (timer > 0)
            {
                timer--;
                return Tuple.Create(bcaType, nextMotionType);
            }
            return Tuple.Create(bcaType, BCMotionType);
        }
    }

    class BattleCharaMotionSeq : BattleCharaMotion
    {
        List<BattleCharaMotion> bcmList;
        public BattleCharaMotionSeq(BattleCharaMotionType bcMotionType, List<BattleCharaMotion> bcmList_)
            : base(bcMotionType)
            => bcmList = bcmList_;

        int index = 0;
        public override Tuple<BattleCharaAnimationType, BattleCharaMotionType> Update(BattleCharaAnimation bcAnim)
        {
            var (retAnimType, retMotionType) = bcmList[index].Update(bcAnim);
            if (retMotionType != BCMotionType)
            {
                index++;
                if (index >= bcmList.Count)
                    return Tuple.Create(retAnimType, retMotionType);
            }
            return Tuple.Create(retAnimType, BCMotionType);
        }
    }
}
