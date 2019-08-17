using System;
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
        //public AttackAttribution Attr;
        public MagicFieldName? MField;
        public Heal(HealFunc heal_, Range.AttackRange aRange,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null, int mp = 0)
            : base(aRange, mp)
        {
            HFunc = heal_;
            //Attr = attr;
            MField = mField;
        }

        /*
        public Heal GenerateHeal(Range.AttackRange aRange)
        {
            var tmp = (Heal)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }
        */

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
            //cEffects = base.CollectConditionedEffects(cEffects);

            Func<Action, List<Occurence>, int, int, List<Occurence>> func = (act, ocrs, source, target) =>
            {
                (RecoverHPf, RecoverMPf) = HFunc(BF.GetCharacter(source).Status, BF.GetCharacter(target).Status);
                //Todo: actから参照する
                if (BF.GetCharacter(target).Status.Dead)
                {
                    ocrs.Add(new Occurence(BF.GetCharacter(target).Name + "は既に倒されている"));
                    return ocrs;
                }
                else
                {

                    BF.GetCharacter(target).Heal(HP: RecoverHP, MP: RecoverMP);

                    string mes = BF.GetCharacter(source).Name + "が";
                    mes += BF.GetCharacter(target).Name + "の";
                    mes += "HPを" + (RecoverHP).ToString() + ", MPを" + (RecoverMP).ToString() + " 回復した";
                    ocrs.Add(new OccurenceDamageForCharacter(mes, target, HPDmg: -RecoverHP, MPDmg: -RecoverMP));
                }
                return ocrs;
            };

            cEffects.Add(new ConditionedEffect(
                (act) => HasSufficientMP,
                (act, ocrs) => ARange.Targets(BF).Aggregate(ocrs, (s, target) => func(act, s, ARange.SourceIndex, target)),
                10000));

            return cEffects;
        }
    }
}
