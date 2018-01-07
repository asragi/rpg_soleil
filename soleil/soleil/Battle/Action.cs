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

        public abstract List<Occurence> Act(BattleField battle, CharacterStatus offence, List<CharacterStatus> deffences);
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
        public AttackForOne(AttackFunc attack_) : base(attack_, TargetCoverage.OneEnemy)
        {

        }

        public override List<Occurence> Act(BattleField battle, CharacterStatus offence, List<CharacterStatus> deffences)
        {
            List<Occurence> ocr = new List<Occurence>();
            if (deffences.Count < 1) return ocr;
            float damage = attack(offence, deffences[0]);
            ocr.Add(new Occurence("nankano damage"));
            return ocr;
        }
    }
}
