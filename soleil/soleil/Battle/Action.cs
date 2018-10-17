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
        public AttackRange ARange
        {
            get; private set;
        }
        public Action(AttackRange aRange)
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

    abstract class Attack : Action
    {
        protected AttackFunc AFunc;
        public AttackAttribution Attr;
        public MagicFieldName? MField;
        public Attack(AttackFunc attack_, AttackRange aRange,  AttackAttribution attr, MagicFieldName? mField) 
            : base(aRange)
        {
            AFunc = attack_;
            Attr = attr;
            MField = mField;
        }
        
    }

    class AttackForOne : Attack
    {
        public AttackForOne(AttackFunc attack_,
            AttackAttribution attr = AttackAttribution.None, MagicFieldName? mField = null) 
            : base(attack_, new OneEnemy(), attr, mField)
        {

        }

        public AttackForOne GenerateAttack(int sourceIndex, int targetIndex)
        {
            var tmp = (AttackForOne)MemberwiseClone();
            if (tmp.ARange is OneEnemy oe)
            {
                oe.SourceIndex = sourceIndex;
                oe.TargetIndex = targetIndex;
                return tmp;
            }
            else throw new Exception("AttackForOne must have OneEnemy");
        }

        public float DamageF;
        public bool HasDamage = false;
        public int Damage
        {
            get { return (int)DamageF; }
        }
        public override List<Occurence> Act(BattleField bf)
        {
            OneEnemy aRange = (OneEnemy)ARange;
            DamageF = AFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);
            HasDamage = true;

            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => true,
                (bfi, act, ocrs) =>
                {
                    //Todo: actから参照する
                    if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
                    {
                        ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                        return ocrs;
                    }
                    else if(!HasDamage)
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
                },
                10000));

            //もっと根幹に組み込むべき条件な気がする
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => bfi.GetCharacter(aRange.TargetIndex).Status.Dead,
                (bfi, act, ocrs) =>
                {
                    bf.RemoveCharacter(aRange.TargetIndex);
                    ocrs.Add(new Occurence(aRange.TargetIndex.ToString() + "はやられた"));
                    return ocrs;
                },
                10001));

            var ocr = AggregateConditionEffects(bf, ceffects);
            return ocr;
        }
    }

    
    abstract class Buff : Action
    {
        protected BuffFunc BFunc;
        public Buff(BuffFunc bFunc, AttackRange aRange) : base(aRange) => BFunc = bFunc;
    }

    class BuffForOne : Buff
    {
        public BuffForOne(BuffFunc buff) : base(buff, new OneEnemy()) { }

        public BuffForOne GenerateAttack(int sourceIndex, int targetIndex)
        {
            var tmp = (BuffForOne)MemberwiseClone();
            if (tmp.ARange is OneEnemy oe)
            {
                oe.SourceIndex = sourceIndex;
                oe.TargetIndex = targetIndex;
                return tmp;
            }
            else throw new Exception("BuffForOne must have OneEnemy");
        }

        public BuffRate BRate;
        public override List<Occurence> Act(BattleField bf)
        {
            OneEnemy aRange = (OneEnemy)ARange;
            BRate = BFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);

            
            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => true,
                (bfi, act, ocrs) =>
                {
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
                },
                10000));
            

            var ocr = AggregateConditionEffects(bf, ceffects);
            return ocr;
        }
    }
    class BuffMe : Buff
    {
        public BuffMe(BuffFunc buff) : base(buff, new Me()) { }

        public BuffMe GenerateAttack(int index)
        {
            var tmp = (BuffMe)MemberwiseClone();
            if (tmp.ARange is Me me)
            {
                me.Index = index;
                return tmp;
            }
            else throw new Exception("BuffMe must have Me");
        }

        public BuffRate BRate;
        public override List<Occurence> Act(BattleField bf)
        {
            Me me = (Me)ARange;
            BRate = BFunc(bf.GetCharacter(me.Index).Status, bf.GetCharacter(me.Index).Status);

            var ceffects = new List<ConditionedEffect>();
            ceffects.Add(new ConditionedEffect(
                (bfi, act) => true,
                (bfi, act, ocrs) =>
                {
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
                },
                10000));


            var ocr = AggregateConditionEffects(bf, ceffects);
            return ocr;
        }
    }
}
