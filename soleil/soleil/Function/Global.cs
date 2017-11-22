using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// いろんな場面で使いたい機能をつめるとこ
    /// </summary>
    static class Global
    {
        static Random rand = new Random();
        /// <summary>
        /// max以下のintをランダムに返す
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int max) => rand.Next(max);
        public static int Random(int min, int max) => rand.Next(min, max);
        public static double RandomDouble() => rand.NextDouble();
        public static double RandomDouble(double max) => rand.NextDouble() * max;
        public static double RandomDouble(double min, double max) => min + rand.NextDouble() * (max - min);
    }
}
