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

    enum TargetCoverage
    {
        OneEnemy,
        AllEnemy,
        Me,
        Ally,
        AllAlly,
        ForAll,
    }

    abstract class Action
    {
        public TargetCoverage target;
        public Action(TargetCoverage target_)
        {
            target = target_;
        }

        public abstract List<Occurence> Act(BattleField battle);
    }

    abstract class Attack : Action
    {
        protected AttackFunc attack;
        public Attack(AttackFunc attack_, TargetCoverage target_) : base(target_)
        {
            attack = attack_;
        }
        
    }

    class AttackForOne : Attack
    {
        int offenceIndex, defenseIndex;
        public AttackForOne(AttackFunc attack_) : base(attack_, TargetCoverage.OneEnemy)
        {

        }

        public AttackForOne GenerateAttack(int offenceIndex, int defenseIndex)
        {
            var tmp = (AttackForOne)MemberwiseClone();
            tmp.offenceIndex = offenceIndex;
            tmp.defenseIndex = defenseIndex;
            return tmp;
        }

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();

            float dmg = attack(bf.GetCharacter(offenceIndex).Status, bf.GetCharacter(defenseIndex).Status);
            int damage = (int)dmg;

            if(bf.GetCharacter(defenseIndex).Status.Dead)
            {
                ocr.Add(new Occurence(defenseIndex.ToString() + "は既に倒している"));
                return ocr;
            }
            else
            {
                string mes = offenceIndex.ToString() + "が";
                mes += defenseIndex.ToString() + "に";
                mes += (damage).ToString() + " ダメージを与えた";
                ocr.Add(new OccurenceDamageForCharacter(mes, defenseIndex, HPDmg: damage));
            }

            bf.GetCharacter(defenseIndex).Damage(HP:damage);
            if (bf.GetCharacter(defenseIndex).Status.Dead)
            {
                bf.RemoveCharacter(defenseIndex);
                ocr.Add(new Occurence(defenseIndex.ToString() + "はやられた"));
            }

            return ocr;
        }
    }

    
    abstract class Buff : Action
    {
        protected BuffFunc buff;
        public Buff(BuffFunc buff_, TargetCoverage target_) : base(target_) => buff = buff_;
    }

    class BuffForOne : Buff
    {
        int offenceIndex, defenseIndex;
        public BuffForOne(BuffFunc buff) : base(buff, TargetCoverage.OneEnemy) { }

        public BuffForOne GenerateAttack(int offenceIndex, int defenseIndex)
        {
            var tmp = (BuffForOne)MemberwiseClone();
            tmp.offenceIndex = offenceIndex;
            tmp.defenseIndex = defenseIndex;
            return tmp;
        }

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();

            var rate = buff(bf.GetCharacter(offenceIndex).Status, bf.GetCharacter(defenseIndex).Status);

            if (bf.GetCharacter(defenseIndex).Status.Dead)
            {
                ocr.Add(new Occurence(defenseIndex.ToString() + "は既に倒している"));
                return ocr;
            }
            else
            {
                string mes = offenceIndex.ToString() + "が";
                mes += defenseIndex.ToString() + "に";
                mes += "バフを与えた";
                ocr.Add(new OccurenceBuffForCharacter(mes, defenseIndex));
            }

            bf.GetCharacter(defenseIndex).Buff(rate);
            if (bf.GetCharacter(defenseIndex).Status.Dead)
            {
                bf.RemoveCharacter(defenseIndex);
                ocr.Add(new Occurence(defenseIndex.ToString() + "はやられた"));
            }

            return ocr;
        }
    }
    class BuffMe : Buff
    {
        int index;
        public BuffMe(BuffFunc buff) : base(buff, TargetCoverage.Me) { }

        public BuffMe GenerateAttack(int index)
        {
            var tmp = (BuffMe)MemberwiseClone();
            tmp.index = index;
            return tmp;
        }

        public override List<Occurence> Act(BattleField bf)
        {
            List<Occurence> ocr = new List<Occurence>();

            var rate = buff(bf.GetCharacter(index).Status, bf.GetCharacter(index).Status);

            if (bf.GetCharacter(index).Status.Dead)
            {
                ocr.Add(new Occurence(index.ToString() + "は既に死んでいる"));
                return ocr;
            }
            else
            {
                string mes = index.ToString() + "は";
                var cmp = rate.Comp();
                if (cmp == 1)
                    mes += "能力が上がった";
                else if (cmp == -1)
                    mes += "能力が下がった";
                else
                    mes += "能力が変動した";
                ocr.Add(new OccurenceBuffForCharacter(mes, index));
            }

            bf.GetCharacter(index).Buff(rate);

            return ocr;
        }
    }
}
