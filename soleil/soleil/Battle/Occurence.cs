using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
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
        public OccurenceDamageForCharacter(string message, int charaIndex, int HPDmg = 0, int MPDmg = 0) : base(message)
        {
            CharaIndex = charaIndex;
            (HPDamage, MPDamage) = (HPDmg, MPDmg);
        }
        public override void Affect()
        {
            if (CharaIndex < 2)
                BF.bcgraphicsList[CharaIndex].Damage(HPDamage, MPDamage);
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
}
