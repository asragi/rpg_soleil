using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class Character
    {
        public CharacterStatus Status;
        protected List<Turn> turns;
        protected BattleField bf;
        protected CommandSelect commandSelect;

        protected int charaIndex;
        public Character(BattleField bField, int index)
        {
            bf = bField;
            charaIndex = index;

            turns = new List<Turn>();
        }

        public bool SelectAction(Turn turn)
        {
            return commandSelect.GetAction(turn);
        }

        public void Damage(int HP = 0, int MP = 0)
        {
            Status.HP -= HP;
            Status.MP -= MP;
        }
        public void Buff(BuffRate rate)
        {
            Status.Rates = rate;
        }
        

        //kari
        public Turn NextTurn()
        {
            var turn = new Turn(Status.NextWaitPoint(), Status, charaIndex);
            turns.Add(turn);
            return turn;
        }
    }
}
