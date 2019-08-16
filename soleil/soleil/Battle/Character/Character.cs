using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    class Character
    {
        public CharacterStatus Status;
        public string Name { get; protected set; }
        protected List<Turn> turns;
        protected static readonly BattleField BF = BattleField.GetInstance();
        protected CommandSelect commandSelect;

        protected int charaIndex;
        public Character(int index)
        {
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
        public void Heal(int HP = 0, int MP = 0)
        {
            Status.HP += HP;
            Status.MP += MP;
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
