using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    /// <summary>
    /// Actionによって生じた情報を持つ
    /// </summary>
    class Occurence
    {
        protected static readonly BattleField BF = BattleField.GetInstance();
        //Effect
        public string Message;
        public int time;
        //Target
        //Damage
        public Occurence(string message, int time = 60)
        {
            Message = message;
            this.time = time;
        }
        //使わないかも
        public virtual void Affect() { }
    }

    class OccurenceDamageForCharacter : Occurence
    {
        public int CharaIndex { get; private set; }
        public int HPDamage = 0, MPDamage = 0;
        public OccurenceDamageForCharacter(string message, int charaIndex, int HPDmg = 0, int MPDmg = 0)
            : base(message, 60)
        {
            CharaIndex = charaIndex;
            (HPDamage, MPDamage) = (HPDmg, MPDmg);
        }
        public override void Affect()
        {
            BF.GetCharacter(CharaIndex).BCGraphics?.Damage(HPDamage, MPDamage);
        }
    }

    class OccurenceAttackMotion : Occurence
    {
        public int CharaIndex { get; private set; }
        public int MPConsume = 0;
        public OccurenceAttackMotion(string message, int charaIndex, int MPConsume_)
            : base(message, 90)
        {
            CharaIndex = charaIndex;
            MPConsume = MPConsume_;
        }
        public override void Affect()
        {
            BF.GetCharacter(CharaIndex).BCGraphics?.Attack(MPConsume);
        }
    }

    class OccurenceEffect : Occurence
    {
        public int CharaIndex { get; private set; }
        EffectAnimationID eaID;
        public OccurenceEffect(string message, int charaIndex, EffectAnimationID eaID)
            : base(message, 50)
        {
            CharaIndex = charaIndex;
            this.eaID = eaID;
        }
        public override void Affect()
        {
            BF.Effects.Add(new AfterCountingEffect(10,
                new AnimationEffect(BF.GetCharacter(CharaIndex).BCGraphics.Pos,
                    new EffectAnimationData(eaID, false, 4),
                    false, BF.Effects),
                BF.Effects));
        }
    }

    class OccurenceBuffForCharacter : Occurence
    {
        public int CharaIndex { get; private set; }
        public int STRrate, VITrate, MAGrate, SPDrate;
        //rate 0...変化なし -1...デバフ 1...バフ?
        public OccurenceBuffForCharacter(string message, int charaIndex, int STRrate = 0, int VITrate = 0, int MAGrate = 0, int SPDrate = 0)
            : base(message, 60)
        {
            CharaIndex = charaIndex;
            (this.STRrate, this.VITrate, this.MAGrate, this.SPDrate) = (STRrate, VITrate, MAGrate, SPDrate);
        }
        public override void Affect()
        {
        }
    }

    class OccurenceForField : Occurence
    {
        public OccurenceForField(string message)
            : base(message, 0)
        {

        }
        public override void Affect() { }
    }

    class OccurenceBattleEnd : Occurence
    {
        public bool DidWin;
        public bool DidLose;
        public OccurenceBattleEnd() : base("", 180)
        {
            DidWin = BF.SameSideIndexes(Side.Left).Count == 0;
            DidLose = BF.SameSideIndexes(Side.Right).Count == 0;
            if (DidWin)
                Message = "戦闘に勝利した";
            else if (DidLose)
                Message = "戦闘に敗北した";

        }
        public OccurenceBattleEnd(string mes)
            : base(mes, 180)
        { }

        public override void Affect()
        {
        }
    }
}
