using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    enum BoolObject
    {
        // Somnia
        Accessary, // アクセサリー売り
        size,
    }
    /// <summary>
    /// 揮発しない，あらゆる場面で共有するboolのリスト．
    /// </summary>
    static class GlobalBoolSet
    {
        static BoolSet[] boolSets;

        static GlobalBoolSet()
        {
            boolSets = new BoolSet[(int)BoolObject.size];
        }

        public static BoolSet Get(BoolObject b, int size)
        {
            // 存在するならそのまま返す．
            if (boolSets[(int)b] != null) return boolSets[(int)b];
            // 存在しないので作って返す．
            var sets = new BoolSet(size);
            boolSets[(int)b] = sets;
            return sets;
        }

        public static void ResetAll() => boolSets = null;
    }
}
