using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    enum BoolEnum
    {
        // Somnia
        MeetAccessary, // アクセサリー売りに会ったことがある．
        size,
    }
    /// <summary>
    /// 揮発しない，あらゆる場面で共有するboolのリスト．
    /// </summary>
    class GlobalBoolSet
    {
        BoolSet boolSet;
        public bool this[BoolEnum i]
        {
            set => boolSet[(int)i] = value;
            get => boolSet[(int)i];
        }

        public GlobalBoolSet()
        {
            boolSet = new BoolSet((int)BoolEnum.size);
        }
    }
}
