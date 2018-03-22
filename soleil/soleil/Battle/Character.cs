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
        List<Turn> turns;
        BattleField bf;
        CommandSelect commandSelect;

        int charaIndex;
        public Character(BattleField bField, int index)
        {
            bf = bField;
            charaIndex = index;

            //てきとう
            var aScore = new AbilityScore(100, 100, 100, 100, 100, 100);
            Status = new CharacterStatus(aScore, 10000);
            turns = new List<Turn>();
            commandSelect = new DefaultCharacterCommandSelect();
        }

        public Action SelectAction()
        {
            return commandSelect.GetAction();
        }

        public void Damage(int HP, int MP)
        {
            Status.HP -= HP;
            Status.MP -= MP;
        }

        //kari
        Reference<int> SPD;
        public Turn NextTurn()
        {
            SPD.Val = Status.SPD;
            var turn = new Turn(Status.NextWaitPoint(), SPD, charaIndex);
            turns.Add(turn);
            return turn;
        }
    }
}
