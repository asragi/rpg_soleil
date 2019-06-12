using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    enum BoolObject
    {
        Global,
        // Somnia
        Accessary, // アクセサリー売り
        size,
    }

    enum GlobalBoolKey
    {
        // Debug
        Test,

        size,
    }

    /// <summary>
    /// 揮発しない，あらゆる場面で共有するboolのリスト．
    /// </summary>
    static class GlobalBoolSet
    {
        // public const int GlobalBoolSize = 255;
        static BoolSet[] boolSets;

        static GlobalBoolSet()
        {
            boolSets = new BoolSet[(int)BoolObject.size];
        }

        public static BoolSet GetBoolSet(BoolObject b, int size)
        {
            var boolSet = boolSets[(int)b];
            // 存在するならそのまま返す．
            if (boolSet != null)
            {
                if (boolSet.Length != size) throw new Exception("BoolSetのsize指定が一致しません．");
                return boolSets[(int)b];
            }
            // 存在しないので作って返す．
            var sets = new BoolSet(size);
            boolSets[(int)b] = sets;
            return sets;
        }

        public static bool Get(BoolObject obj, int target) => boolSets[(int)obj][target];

        public static void ResetAll() => boolSets = null;
    }
}
