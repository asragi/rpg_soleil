using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    class MathEx
    {
        public const double FullCircle = 360;
        public const double Eps = 1.0e-5;
        public const double Rad = Math.PI / 180;

        //Check e1⊇e2
        public static bool Contains<T>(IEnumerable<T> e1, IEnumerable<T> e2)
        {
            foreach (var e in e2)
                if (!e1.Contains(e)) return false;
            return true;
        }

        public static double DegreeNormalize(double degree)
        {
            //degree -> 0~360

            while (degree >= FullCircle) degree -= FullCircle;
            while (degree < 0) degree += FullCircle;
            return degree;
        }

        public static bool IsZero(double x) => Math.Abs(x) < Eps;

        // Clamp
        public static T Clamp<T>(T value, T max, T min) where T : IComparable<T>
        {
            if (value.CompareTo(max) > 0) return max;
            if (value.CompareTo(min) < 0) return min;
            return value;
        }

        //if x>0 then x-y else x+y
        public static double AbsoluteMinus(double x, double y)
        {
            if (x > y)
                return x - y;
            else if (x < -y)
                return x + y;
            else return 0;
        }

        public static bool IsInRange(double x, double y, double z) => x <= y && y < z;

        /// <summary>
        /// trueが存在するかチェックする
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool TrueExist(IEnumerable<bool> e) => e.Aggregate((i,j)=>i||j);

        public static float ToRadian(double r) => (float)(r * Rad);
        public static double Sin(double r) => Math.Sin(r * Rad);
        public static double Cos(double r) => Math.Cos(r * Rad);
        public static double Tan(double r) => Math.Tan(r * Rad);
        public static double Atan2(double y,double x) => Math.Atan2(y,x)/Rad;
    }
}
