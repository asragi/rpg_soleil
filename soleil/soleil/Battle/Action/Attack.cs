using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    using AttackFunc = Func<CharacterStatus, CharacterStatus, float>;
    enum AttackAttribution
    {
        None = -1,
        Beat,
        Cut,
        Thrust,
        Fever,
        Ice,
        Electro,
        size,
    }

    static class ExtendAttackAttribution
    {
        static readonly Dictionary<AttackAttribution, String> dict = new Dictionary<AttackAttribution, string>
        {
            {AttackAttribution.Beat, "打撃"},
            {AttackAttribution.Cut, "斬撃"},
            {AttackAttribution.Thrust, "弾丸"},
            {AttackAttribution.Fever, "熱"},
            {AttackAttribution.Ice, "冷気"},
            {AttackAttribution.Electro, "電撃"},
        };

        public static String Name(this AttackAttribution att) => dict[att];
    }

    class Attack : Action
    {
        protected AttackFunc AFunc;
        public AttackAttribution Attr;
        public MagicFieldName? MField;
        public Attack(AttackFunc attack_, Range.AttackRange aRange,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null)
            : base(aRange)
        {
            AFunc = attack_;
            Attr = attr;
            MField = mField;
        }

        public Attack GenerateAttack(Range.AttackRange aRange)
        {
            var tmp = (Attack)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        public float DamageF;
        public bool HasDamage = false;
        public int Damage
        {
            get { return (int)DamageF; }
        }
        public override List<Occurence> Act(BattleField bf)
        {
            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    DamageF = AFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);
                    break;
            }
            HasDamage = true;

            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => true,
                (bfi, act, ocrs) =>
                {
                    switch (act.ARange)
                    {
                        case Range.OneEnemy aRange:
                            //Todo: actから参照する
                            if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
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
                                bf.GetCharacter(aRange.TargetIndex).Damage(HP: Damage);

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

            //もっと根幹に組み込むべき条件な気がする
            var alives = bf.AliveIndexes();
            alives.ForEach(p => ceffects.Add(new ConditionedEffect(
                (bfi, act) => bfi.GetCharacter(p).Status.Dead,
                (bfi, act, ocrs) =>
                {
                    bf.RemoveCharacter(p);
                    ocrs.Add(new Occurence(p.ToString() + "はやられた"));
                    return ocrs;
                },
                10001)));

            var ocr = AggregateConditionEffects(bf, ceffects);
            return ocr;
        }
    }
}
