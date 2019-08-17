using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    using BuffFunc = Func<CharacterStatus, CharacterStatus, BuffRate>;
    /// <summary>
    /// バフ・デバフを表す行動
    /// </summary>
    class Buff : Action
    {
        protected BuffFunc BFunc;
        public Buff(BuffFunc bFunc, Range.AttackRange aRange, int mp = 0) : base(aRange, mp) => BFunc = bFunc;

        /*
        public Buff GenerateBuff(Range.AttackRange aRange)
        {
            var tmp = (Buff)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }
        */

        public BuffRate BRate;
        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            //cEffects = base.CollectConditionedEffects(cEffects);

            Func<Action, List<Occurence>, int, int, List<Occurence>> buffForEnemy = (act, ocrs, source, target) =>
            {
                BRate = BFunc(BF.GetCharacter(source).Status, BF.GetCharacter(target).Status);
                if (BF.GetCharacter(target).Status.Dead)
                {
                    ocrs.Add(new Occurence(BF.GetCharacter(target).Name + "は既に倒している"));
                }
                else
                {
                    BF.GetCharacter(target).Buff(BRate);
                    string mes = BF.GetCharacter(source).Name + "が";
                    mes += BF.GetCharacter(target).Name + "に";
                    mes += "バフを与えた";
                    ocrs.Add(new OccurenceBuffForCharacter(mes, target));
                }
                return ocrs;
            }
            ,
            buffForAlly = (act, ocrs, source, target) =>
            {
                BRate = BFunc(BF.GetCharacter(source).Status, BF.GetCharacter(target).Status);
                if (BF.GetCharacter(target).Status.Dead)
                {
                    ocrs.Add(new Occurence(BF.GetCharacter(target).Name + "は既に倒されている"));
                }
                else
                {
                    BF.GetCharacter(target).Buff(BRate);
                    string mes = BF.GetCharacter(source).Name + "が";
                    mes += BF.GetCharacter(target).Name + "に";
                    mes += "バフを与えた";
                    ocrs.Add(new OccurenceBuffForCharacter(mes, target));
                }
                return ocrs;
            }
            ,
            buffForMe = (act, ocrs, source, target) =>
            {
                BRate = BFunc(BF.GetCharacter(source).Status, BF.GetCharacter(target).Status);
                if (BF.GetCharacter(source).Status.Dead)
                {
                    ocrs.Add(new Occurence(BF.GetCharacter(source).Name + "は既に死んでいる"));
                }
                else
                {
                    BF.GetCharacter(source).Buff(BRate);
                    string mes = BF.GetCharacter(source).Name + "は";
                    var cmp = BRate.Comp();
                    if (cmp == 1)
                        mes += "能力が上がった";
                    else if (cmp == -1)
                        mes += "能力が下がった";
                    else
                        mes += "能力が変動した";
                    ocrs.Add(new OccurenceBuffForCharacter(mes, source));
                }
                return ocrs;
            };


            cEffects.Add(new ConditionedEffect(
                (act) => HasSufficientMP,
                (act, ocrs) =>
                {
                    switch (act.ARange)
                    {
                        case Range.OneEnemy _:
                        case Range.AllEnemy _:
                            return ARange.Targets(BF).Aggregate(ocrs, (s, target) => buffForEnemy(act, s, ARange.SourceIndex, target));

                        case Range.Ally _:
                        case Range.AllAlly _:
                            return ARange.Targets(BF).Aggregate(ocrs, (s, target) => target == ARange.SourceIndex ? buffForMe(act, s, ARange.SourceIndex, target) : buffForAlly(act, s, ARange.SourceIndex, target));
                        case Range.Me _:
                            return buffForMe(act, ocrs, ARange.SourceIndex, ARange.SourceIndex);

                        case Range.ForAll _:
                            ocrs = BF.OppositeIndexes(ARange.SourceIndex).Aggregate(ocrs, (s, target) => buffForEnemy(act, s, ARange.SourceIndex, target));
                            return BF.SameSideIndexes(ARange.SourceIndex).Aggregate(ocrs, (s, target) => target == ARange.SourceIndex ? buffForMe(act, s, ARange.SourceIndex, target) : buffForAlly(act, s, ARange.SourceIndex, target));

                        case Range.ForOthers _:
                            ocrs = BF.OppositeIndexes(ARange.SourceIndex).Aggregate(ocrs, (s, target) => buffForEnemy(act, s, ARange.SourceIndex, target));
                            return BF.SameSideIndexes(ARange.SourceIndex).Aggregate(ocrs, (s, target) => target == ARange.SourceIndex ? ocrs : buffForAlly(act, s, ARange.SourceIndex, target));

                        default:
                            throw new Exception("not implemented");
                    }
                },
                10000));

            return cEffects;
        }
    }
}
