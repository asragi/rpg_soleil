﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    using HealFunc = Func<CharacterStatus, CharacterStatus, Tuple<float, float>>;

    /// <summary>
    /// HP,MPの回復行動
    /// </summary>
    class Heal : Action
    {
        protected HealFunc HFunc;
        public AttackAttribution Attr; //これいる？
        public MagicFieldName? MField;
        public Heal(HealFunc heal_, Range.AttackRange aRange,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null, int mp = 0)
            : base(aRange, mp)
        {
            HFunc = heal_;
            Attr = attr;
            MField = mField;
        }

        public Heal GenerateHeal(Range.AttackRange aRange)
        {
            var tmp = (Heal)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        public float RecoverHPf;
        public int RecoverHP
        {
            get { return (int)RecoverHPf; }
        }
        public float RecoverMPf;
        public int RecoverMP
        {
            get { return (int)RecoverMPf; }
        }

        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            cEffects = base.CollectConditionedEffects(cEffects);

            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    (RecoverHPf, RecoverMPf) = HFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.TargetIndex).Status);
                    break;
                case Range.Me aRange:
                    (RecoverHPf, RecoverMPf) = HFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.SourceIndex).Status);
                    break;
                case Range.Ally aRange:
                    (RecoverHPf, RecoverMPf) = HFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.TargetIndex).Status);
                    break;
            }

            cEffects.Add(new ConditionedEffect(
                (act) => HasSufficientMP,
                (act, ocrs) =>
                {
                    switch (act.ARange)
                    {
                        case Range.Ally aRange:
                            //Todo: actから参照する
                            if (BF.GetCharacter(aRange.TargetIndex).Status.Dead)
                            {
                                ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒されている"));
                                return ocrs;
                            }
                            else
                            {

                                BF.GetCharacter(aRange.TargetIndex).Heal(HP: RecoverHP, MP: RecoverMP);

                                string mes = aRange.SourceIndex.ToString() + "が";
                                mes += aRange.TargetIndex.ToString() + "の";
                                mes += "HPを" + (RecoverHP).ToString() + ", MPを" + (RecoverMP).ToString() + " 回復した";
                                ocrs.Add(new OccurenceDamageForCharacter(mes, aRange.TargetIndex, HPDmg: -RecoverHP, MPDmg: -RecoverMP));
                            }
                            return ocrs;
                        default:
                            throw new Exception("not implemented");
                    }
                },
                10000));

            return cEffects;
        }
    }
}