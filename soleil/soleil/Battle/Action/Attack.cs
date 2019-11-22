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
        /// <summary>
        /// 自分と相手のStatusから威力を計算する関数
        /// </summary>
        protected AttackFunc AFunc;
        public AttackAttribution Attr;
        public MagicFieldName? MField;
        EffectAnimationID eaID;
        public Attack(AttackFunc attack_, Range.AttackRange aRange, EffectAnimationID eaID,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null, int mp = 0)
            : base(aRange, mp)
        {
            AFunc = attack_;
            this.eaID = eaID;
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

        public bool HasDamage = false;

        /// <summary>
        /// 攻撃ダメージの計算結果
        /// ダメージ軽減等の計算の為にpublicにしています
        /// </summary>
        public float DamageF;

        /// <summary>
        /// 実際に与えるダメージ
        /// </summary>
        public int Damage
        {
            get { return (int)DamageF; }
        }


        public override List<ConditionedEffect> CollectConditionedEffects(List<ConditionedEffect> cEffects)
        {
            //cEffects = base.CollectConditionedEffects(cEffects);
            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    break;
            }
            HasDamage = true;
            Func<Action, List<Occurence>, int, int, List<Occurence>> func = (act, ocrs, source, target) =>
            {
                ocrs.Add(new OccurenceEffect("", target, eaID));
                DamageF = AFunc(BF.GetCharacter(source).Status, BF.GetCharacter(target).Status);
                //Todo: actから参照する
                if (BF.GetCharacter(target).Status.Dead)
                {
                    ocrs.Add(new Occurence(BF.GetCharacter(target).Name + "は既に倒している"));
                    return ocrs;
                }
                else if (!HasDamage)
                {
                    //効果はないor消されたパターン
                    string mes = BF.GetCharacter(source).Name + "が";
                    mes += BF.GetCharacter(target).Name + "に";
                    mes += 0.ToString() + " ダメージを与えた";
                    ocrs.Add(new OccurenceDamageForCharacter(mes, target, HPDmg: Damage));
                }
                else
                {
                    BF.GetCharacter(target).Damage(HP: Damage);

                    string mes = BF.GetCharacter(source).Name + "が";
                    mes += BF.GetCharacter(target).Name + "に";
                    mes += Damage.ToString() + " ダメージを与えた";
                    ocrs.Add(new OccurenceDamageForCharacter(mes, target, HPDmg: Damage));
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
