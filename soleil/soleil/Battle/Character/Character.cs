using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    enum CharacterType
    {
        Lune,
        Sunny,
        Tella,

        TestEnemy,
        Size,
    }

    class Character
    {
        public CharacterStatus Status;
        public string Name { get; protected set; }
        protected List<Turn> turns;
        protected static readonly BattleField BF = BattleField.GetInstance();
        protected CommandSelect commandSelect;
        public CharacterType CharacterType;
        public BattleCharaGraphics BCGraphics;

        protected int CharacterIndex;
        public Character(int index, CharacterType charaType)
        {
            CharacterIndex = index;
            CharacterType = charaType;
            turns = new List<Turn>();
        }

        public virtual Character Generate(int index)
        {
            var tmp = (Character)MemberwiseClone();
            tmp.CharacterIndex = index;
            return tmp;
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

        public void Win() => BCGraphics?.Win();
        public void Update() => BCGraphics?.Update();
        public void Draw(Drawing d) => BCGraphics?.Draw(d);


        //kari
        /// <summary>
        /// 次の行動選択Turnを、WPを計算して返す
        /// </summary>
        public Turn NextTurn()
        {
            var turn = new Turn(Status.NextWaitPoint(), Status, CharacterIndex);
            turns.Add(turn);
            return turn;
        }
    }
}
