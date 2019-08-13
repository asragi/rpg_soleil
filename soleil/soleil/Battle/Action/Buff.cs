using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    using BuffFunc = Func<CharacterStatus, CharacterStatus, BuffRate>;
    /// <summary>
    /// バフ・デバフを表す行動
    /// </summary>
    class Buff : Action
    {
        protected BuffFunc BFunc;
        public Buff(BuffFunc bFunc, Range.AttackRange aRange, int mp = 0) : base(aRange, mp) => BFunc = bFunc;

        public Buff GenerateAttack(Range.AttackRange aRange)
        {
            var tmp = (Buff)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        public BuffRate BRate;
        public override List<Occurence> Act()
        {
            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    BRate = BFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.TargetIndex).Status);
                    break;
                case Range.Me aRange:
                    BRate = BFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.SourceIndex).Status);
                    break;
            }


            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (act) => true,
                (act, ocrs) =>
                {
                    //MP消費
                    if (MP <= BF.GetCharacter(act.ARange.SourceIndex).Status.MP)
                    {
                        BF.GetCharacter(act.ARange.SourceIndex).Damage(MP: MP);
                        string mes = act.ARange.SourceIndex.ToString() + "の攻撃！";
                        ocrs.Add(new OccurenceAttackMotion(mes, act.ARange.SourceIndex, MPConsume_: MP));
                    }
                    else
                    {
                        ocrs.Add(new Occurence(act.ARange.SourceIndex.ToString() + "はMPが不足している"));
                        return ocrs;
                    }
                    switch (act.ARange)
                    {
                        case Range.OneEnemy aRange:
                            if (BF.GetCharacter(aRange.TargetIndex).Status.Dead)
                            {
                                ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                            }
                            else
                            {
                                BF.GetCharacter(aRange.TargetIndex).Buff(BRate);
                                string mes = aRange.SourceIndex.ToString() + "が";
                                mes += aRange.TargetIndex.ToString() + "に";
                                mes += "バフを与えた";
                                ocrs.Add(new OccurenceBuffForCharacter(mes, aRange.TargetIndex));
                            }
                            return ocrs;
                        case Range.Me me:
                            if (BF.GetCharacter(me.SourceIndex).Status.Dead)
                            {
                                ocrs.Add(new Occurence(me.SourceIndex.ToString() + "は既に死んでいる"));
                            }
                            else
                            {
                                BF.GetCharacter(me.SourceIndex).Buff(BRate);
                                string mes = me.SourceIndex.ToString() + "は";
                                var cmp = BRate.Comp();
                                if (cmp == 1)
                                    mes += "能力が上がった";
                                else if (cmp == -1)
                                    mes += "能力が下がった";
                                else
                                    mes += "能力が変動した";
                                ocrs.Add(new OccurenceBuffForCharacter(mes, me.SourceIndex));
                            }
                            return ocrs;
                        default:
                            throw new Exception("not implemented");
                    }
                },
                10000));


            var ocr = AggregateConditionEffects(ceffects);
            return ocr;
        }
    }
}
