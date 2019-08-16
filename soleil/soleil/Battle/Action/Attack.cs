using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    using AttackFunc = Func<CharacterStatus, CharacterStatus, float>;

    /// <summary>
    /// ターンでの攻撃行動
    /// </summary>
    class Attack : Action
    {
        protected AttackFunc AFunc;
        public AttackAttribution Attr;
        public MagicFieldName? MField;
        public Attack(AttackFunc attack_, Range.AttackRange aRange,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null, int mp = 0)
            : base(aRange, mp)
        {
            AFunc = attack_;
            Attr = attr;
            MField = mField;
        }

        /*
        public Attack GenerateAttack(Range.AttackRange aRange)
        {
            var tmp = (Attack)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }
        */

        public float DamageF;
        public bool HasDamage = false;
        public int Damage
        {
            get { return (int)DamageF; }
        }
        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            cEffects = base.CollectConditionedEffects(cEffects);
            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    DamageF = AFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.TargetIndex).Status);
                    break;
            }
            HasDamage = true;

            cEffects.Add(new ConditionedEffect(
                (act) => HasSufficientMP,
                (act, ocrs) =>
                {
                    switch (act.ARange)
                    {
                        case Range.OneEnemy aRange:
                            //Todo: actから参照する
                            if (BF.GetCharacter(aRange.TargetIndex).Status.Dead)
                            {
                                ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                                return ocrs;
                            }
                            else if (!HasDamage)
                            {
                                //効果はないor消されたパターン
                                string mes = aRange.SourceIndex.ToString() + "が";
                                mes += aRange.TargetIndex.ToString() + "に";
                                mes += 0.ToString() + " ダメージを与えた";
                                ocrs.Add(new OccurenceDamageForCharacter(mes, aRange.TargetIndex, HPDmg: Damage));
                            }
                            else
                            {
                                BF.GetCharacter(aRange.TargetIndex).Damage(HP: Damage);

                                string mes = aRange.SourceIndex.ToString() + "が";
                                mes += aRange.TargetIndex.ToString() + "に";
                                mes += (Damage).ToString() + " ダメージを与えた";
                                ocrs.Add(new OccurenceDamageForCharacter(mes, aRange.TargetIndex, HPDmg: Damage));
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
