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

        /// <summary>
        /// 行動選択ターンが来たときに呼び出される
        /// </summary>
        /// <returns> 選択が完了したかどうか </returns>
        public bool SelectAction(Turn turn)
        {
            return commandSelect.GetAction(turn);
        }

        /// <summary>
        /// ダメージを与える
        /// 0 <= HP < MAXHP は勝手に丸めてくれる
        /// </summary>
        public void Damage(int HP = 0, int MP = 0)
        {
            Status.HP -= HP;
            Status.MP -= MP;
        }


        /// <summary>
        /// 回復する
        /// 0 <= HP < MAXHP は勝手に丸めてくれる
        /// </summary>
        public void Heal(int HP = 0, int MP = 0)
        {
            Status.HP += HP;
            Status.MP += MP;
        }

        /// <summary>
        /// Buffを与える(代入する?)
        /// </summary>
        public void Buff(BuffRate rate)
        {
            Status.Rates = rate;
        }


        //kari
        /// <summary>
        /// 次の行動選択Turnを、WPを計算して返す
        /// </summary>
        public Turn NextTurn()
        {
            var turn = new Turn(Status.NextWaitPoint(), Status, charaIndex);
            turns.Add(turn);
            return turn;
        }
    }
}
