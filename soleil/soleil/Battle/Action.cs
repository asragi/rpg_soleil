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

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();
            OneEnemy aRange = (OneEnemy)ARange;

            float dmg = AFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);
            int damage = (int)dmg;

            if(bf.GetCharacter(aRange.TargetIndex).Status.Dead)
            {
                ocr.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                return ocr;
            }
            else
            {
                string mes = aRange.SourceIndex.ToString() + "が";
                mes += aRange.TargetIndex.ToString() + "に";
                mes += (damage).ToString() + " ダメージを与えた";
                ocr.Add(new OccurenceDamageForCharacter(mes, aRange.TargetIndex, HPDmg: damage));
            }

            bf.GetCharacter(aRange.TargetIndex).Damage(HP:damage);
            if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
            {
                bf.RemoveCharacter(aRange.TargetIndex);
                ocr.Add(new Occurence(aRange.TargetIndex.ToString() + "はやられた"));
            }

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

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();
            OneEnemy aRange = (OneEnemy)ARange;

            var rate = BFunc(bf.GetCharacter(aRange.SourceIndex).Status, bf.GetCharacter(aRange.TargetIndex).Status);

            if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
            {
                ocr.Add(new Occurence(aRange.TargetIndex.ToString() + "は既に倒している"));
                return ocr;
            }
            else
            {
                string mes = aRange.SourceIndex.ToString() + "が";
                mes += aRange.TargetIndex.ToString() + "に";
                mes += "バフを与えた";
                ocr.Add(new OccurenceBuffForCharacter(mes, aRange.TargetIndex));
            }

            bf.GetCharacter(aRange.TargetIndex).Buff(rate);
            if (bf.GetCharacter(aRange.TargetIndex).Status.Dead)
            {
                bf.RemoveCharacter(aRange.TargetIndex);
                ocr.Add(new Occurence(aRange.TargetIndex.ToString() + "はやられた"));
            }

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

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();
            Me me = (Me)ARange;

            var rate = BFunc(bf.GetCharacter(me.Index).Status, bf.GetCharacter(me.Index).Status);

            if (bf.GetCharacter(me.Index).Status.Dead)
            {
                ocr.Add(new Occurence(me.Index.ToString() + "は既に死んでいる"));
                return ocr;
            }
            else
            {
                string mes = me.Index.ToString() + "は";
                var cmp = rate.Comp();
                if (cmp == 1)
                    mes += "能力が上がった";
                else if (cmp == -1)
                    mes += "能力が下がった";
                else
                    mes += "能力が変動した";
                ocr.Add(new OccurenceBuffForCharacter(mes, me.Index));
            }

            bf.GetCharacter(me.Index).Buff(rate);

            return ocr;
        }
    }
}
