using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
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
        public override List<Occurence> Act()
        {
            switch (ARange)
            {
                case Range.OneEnemy aRange:
                    DamageF = AFunc(BF.GetCharacter(aRange.SourceIndex).Status, BF.GetCharacter(aRange.TargetIndex).Status);
                    break;
            }
            HasDamage = true;

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

            //もっと根幹に組み込むべき条件な気がする
            var alives = BF.AliveIndexes();
            alives.ForEach(p => ceffects.Add(new ConditionedEffect(
                (act) => BF.GetCharacter(p).Status.Dead,
                (act, ocrs) =>
                {
                    BF.RemoveCharacter(p);
                    ocrs.Add(new Occurence(p.ToString() + "はやられた"));
                    return ocrs;
                },
                10001)));

            var ocr = AggregateConditionEffects(ceffects);
            return ocr;
        }
    }
}
