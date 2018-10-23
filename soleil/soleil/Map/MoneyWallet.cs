using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// 所持金を保持するクラス．
    /// </summary>
    class MoneyWallet
    {
        public const String Currency = "Ac.";
        private int val;
        public int Val { get { return val; } private set { val = value; } }

        public MoneyWallet(int initVal)
        {
            val = initVal;
        }

        /// <summary>
        /// 所持金を増やす．
        /// </summary>
        public void Add(int num) => val += num;

        /// <summary>
        /// 指定された金額を所持しているかどうかを返す．
        /// </summary>
        public bool HasEnough(int num) => val >= num;

        /// <summary>
        /// 所持金を減らす．足りない場合は減らしてfalseを返す．
        /// 足りない場合に減らす処理を行う場合はほぼないと思うので，必ずHasEnoughで確認したい．
        /// </summary>
        /// <returns>お金が足りたかどうか．</returns>
        public bool Consume(int num)
        {
            var enough = val >= num;
            val = enough ? val - num : 0;
            return enough;
        }
    }
}
