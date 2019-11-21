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
        public bool Visible { get; set; }
        //Target
        //Damage
        public Occurence(string message)
        {
            Message = message;
        }
        //使わないかも
        public virtual void Affect() { }
    }

    class OccurenceDamageForCharacter : Occurence
    {
        public int CharaIndex { get; private set; }
        public int HPDamage = 0, MPDamage = 0;
        EffectAnimationID eaID;
        public OccurenceDamageForCharacter(string message, int charaIndex, EffectAnimationID eaID, int HPDmg = 0, int MPDmg = 0) : base(message)
        {
            CharaIndex = charaIndex;
            this.eaID = eaID;
            (HPDamage, MPDamage) = (HPDmg, MPDmg);
        }
        public override void Affect()
        {
            BF.GetCharacter(CharaIndex).BCGraphics?.Damage(HPDamage, MPDamage);

            BF.Effects.Add(new AfterCountingEffect(90,
                new AnimationEffect(BF.GetCharacter(CharaIndex).BCGraphics.Pos,
                    new EffectAnimationData(eaID, false, 4),
                    false, BF.Effects),
                BF.Effects));
        }
    }

    class OccurenceAttackMotion : Occurence
    {
        public int CharaIndex { get; private set; }
        public int MPConsume = 0;
        public OccurenceAttackMotion(string message, int charaIndex, int MPConsume_) : base(message)
        {
            CharaIndex = charaIndex;
            MPConsume = MPConsume_;
        }
        public override void Affect()
        {
            BF.GetCharacter(CharaIndex).BCGraphics?.Attack(MPConsume);
        }
    }

    class OccurenceBuffForCharacter : Occurence
    {
        public int CharaIndex { get; private set; }
        public int STRrate, VITrate, MAGrate, SPDrate;
        //rate 0...変化なし -1...デバフ 1...バフ?
        public OccurenceBuffForCharacter(string message, int charaIndex, int STRrate = 0, int VITrate = 0, int MAGrate = 0, int SPDrate = 0) : base(message)
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
        public OccurenceForField(string message) : base(message)
        {

        }
        public override void Affect() { }
    }

    class OccurenceBattleEnd : Occurence
    {
        public bool DidWin;
        public bool DidLose;
        public OccurenceBattleEnd() : base("")
        {
            DidWin = BF.SameSideIndexes(Side.Left).Count == 0;
            DidLose = BF.SameSideIndexes(Side.Right).Count == 0;
            if (DidWin)
                Message = "戦闘に勝利した";
            else if (DidLose)
                Message = "戦闘に敗北した";

        }
        public OccurenceBattleEnd(string mes) : base(mes)
        { }

        public override void Affect()
        {
        }
    }
}
