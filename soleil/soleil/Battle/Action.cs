using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil
{
    using AttackFunc = Func<CharacterStatus, CharacterStatus, float>;
    using BuffFunc = Func<CharacterStatus, CharacterStatus, BuffRate>;

    enum ActionName
    {
        //Attack
        NormalAttack,
        ExampleMagic,

        //Buff
        Guard,
        EndGuard,
        ExampleDebuff,

        Size,
    }

    enum AttackAttribution
    {
        None,
        Beat,
        Cut,
        Thrust,
        Fever,
        Ice,
        Electro,
    }

    abstract class Action
    {
        public Range.AttackRange ARange
        {
            get; protected set;
        }
        public Action(Range.AttackRange aRange)
        {
            ARange = aRange;
        }

        public abstract List<Occurence> Act(BattleField battle);


        public List<Occurence> AggregateConditionEffects(BattleField bf, IEnumerable<ConditionedEffect> additionals, List<Occurence> ocr)
        {
            var ceffects = bf.GetCopiedCEffects();
            ceffects.UnionWith(additionals);
            return ceffects.Aggregate(ocr, (ocrs, ce) => ce.Act(bf, this, ocrs));
        }
        public List<Occurence> AggregateConditionEffects(BattleField bf, IEnumerable<ConditionedEffect> additionals)
        {
            return AggregateConditionEffects(bf, additionals, new List<Occurence>());
        }
    }

    class Attack : Action
    {
        protected AttackFunc AFunc;
        public AttackAttribution Attr;
        public MagicFieldName? MField;
        public Attack(AttackFunc attack_, Range.AttackRange aRange, 
            AttackAttribution attr=AttackAttribution.None, MagicFieldName? mField=null) 
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

    
    class Buff : Action
    {
        protected BuffFunc BFunc;
        public Buff(BuffFunc bFunc, Range.AttackRange aRange) : base(aRange) => BFunc = bFunc;

        public Buff GenerateAttack(Range.AttackRange aRange)
        {
            var tmp = (Buff)MemberwiseClone();
            tmp.ARange = aRange;
            return tmp;
        }

        public BuffRate BRate;
        public override List<Occurence> Act(BattleField bf)
        {
            switch(ARange)
            {
                case Range.OneEnemy aRange:
                    BRate = BFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);
                    break;
                case Range.Me aRange:
                    BRate = BFunc(bf.GetCharacter(aRange.Index).Status, bf.GetCharacter(aRange.Index).Status);
                    break;
            }


            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => true,
                (bfi, act, ocrs) =>
                {
                    switch (act.ARange)
                    {
                        case Range.OneEnemy aRange:
                            if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
                            {
                                ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                            }
                            else
                            {
                                bf.GetCharacter(aRange.TargetIndex).Buff(BRate);
                                string mes = aRange.SourceIndex.ToString() + "が";
                                mes += aRange.TargetIndex.ToString() + "に";
                                mes += "バフを与えた";
                                ocrs.Add(new OccurenceBuffForCharacter(mes, aRange.TargetIndex));
                            }
                            return ocrs;
                        case Range.Me me:
                            if (bf.GetCharacter(me.Index).Status.Dead)
                            {
                                ocrs.Add(new Occurence(me.Index.ToString() + "は既に死んでいる"));
                            }
                            else
                            {
                                bf.GetCharacter(me.Index).Buff(BRate);
                                string mes = me.Index.ToString() + "は";
                                var cmp = BRate.Comp();
                                if (cmp == 1)
                                    mes += "能力が上がった";
                                else if (cmp == -1)
                                    mes += "能力が下がった";
                                else
                                    mes += "能力が変動した";
                                ocrs.Add(new OccurenceBuffForCharacter(mes, me.Index));
                            }
                            return ocrs;
                        default:
                            throw new Exception("not implemented");
                    }
                },
                10000));


            var ocr = AggregateConditionEffects(bf, ceffects);
            return ocr;
        }
    }
}
