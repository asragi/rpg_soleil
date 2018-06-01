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
        //Effect
        public string Message;
        public bool Visible { get; set; }
        //Target
        //Damage
        public Occurence(string message)
        {
            Message = message;
        }
        public virtual void Affect(BattleField bf) { }
    }

    class OccurenceForCharacter : Occurence
    {
        public int CharaIndex { get; private set; }
        public int HPDamage = 0, MPDamage = 0;
        public OccurenceForCharacter(string message, int charaIndex, int HPDmg = 0, int MPDmg = 0):base(message)
        {
            CharaIndex = charaIndex;
            (HPDamage, MPDamage) = (HPDmg, MPDmg);
        }
        public override void Affect(BattleField bf)
        {
            bf.GetCharacter(CharaIndex).Damage(HPDamage, MPDamage);
        }
    }

    class OccurenceForField : Occurence
    {
        public OccurenceForField(string message) : base(message)
        {

        }
        public override void Affect(BattleField bf) { }
    }
}
