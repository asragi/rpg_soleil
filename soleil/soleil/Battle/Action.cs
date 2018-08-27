using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soleil
{
    using AttackFunc = Func<CharacterStatus, CharacterStatus, float>;
    enum ActionName
    {
        NormalAttack,
        ExampleMagic,

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
                ocr.Add(new OccurenceForCharacter(mes, defenseIndex, HPDmg: damage));
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
}
