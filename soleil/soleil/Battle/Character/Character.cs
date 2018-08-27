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
            SPD = new Reference<int>();
        }

        public Action SelectAction()
        {
            return commandSelect.GetAction();
        }

        public void Damage(int HP=0, int MP=0)
        {
            Status.HP -= HP;
            Status.MP -= MP;
        }

        /// <summary>
        /// デバフ攻撃を受ける
        /// </summary>
        public void Debuff()
        {

            //SPDを更新
            SPD.Val = Status.SPD;
        }

        //kari
        protected Reference<int> SPD;
        public Turn NextTurn()
        {
            SPD.Val = Status.SPD;
            var turn = new Turn(Status.NextWaitPoint(), SPD, charaIndex);
            turns.Add(turn);
            return turn;
        }
    }
}
