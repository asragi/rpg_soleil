using System;

namespace Soleil
{
    static class Easing
    {
        public static double InQuad(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return max * t * t + min;
        }

        public static double OutQuad(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return -max * t * (t - 2) + min;
        }

        public static double InOutQuad(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            if (t / 2 < 1)
                return max / 2 * t * t + min;
            --t;
            return -max * (t * (t - 2) - 1) + min;
        }

        public static double InCubic(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return max * t * t * t + min;
        }

        public static double OutCubic(double t, double totaltime, double max, double min)
        {
            max -= min;
            t = t / totaltime - 1;
            return max * (t * t * t + 1) + min;
        }

        public static double InOutCubic(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            if (t / 2 < 1)
                return max / 2 * t * t * t + min;
            t -= 2;
            return max / 2 * (t * t * t + 2) + min;
        }

        public static double InQuart(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return max * t * t * t * t + min;
        }
        public static double OutQuart(double t, double totaltime, double max, double min)
        {
            max -= min;
            t = t / totaltime - 1;
            return -max * (t * t * t * t - 1) + min;
        }
        public static double InOutQuart(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            if (t / 2 < 1)
                return max / 2 * t * t * t * t + min;
            t -= 2;
            return -max / 2 * (t * t * t * t - 2) + min;
        }
        public static double InQuint(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return max * t * t * t * t * t + min;
        }
        public static double OutQuint(double t, double totaltime, double max, double min)
        {
            max -= min;
            t = t / totaltime - 1;
            return max * (t * t * t * t * t + 1) + min;
        }
        public static double InOutQuint(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            if (t / 2 < 1)
                return max / 2 * t * t * t * t * t + min;
            t -= 2;
            return max / 2 * (t * t * t * t * t + 2) + min;
        }
        public static double InSine(double t, double totaltime, double max, double min)
        {
            max -= min;
            return -max * Math.Cos(t * (Math.PI/2) / totaltime) + max + min;
        }
        public static double OutSine(double t, double totaltime, double max, double min)
        {
            max -= min;
            return max * Math.Sin(t * (Math.PI / 2) / totaltime) + min;
        }
        public static double InOutSine(double t, double totaltime, double max, double min)
        {
            max -= min;
            return -max / 2 * (Math.Cos(t * Math.PI / totaltime) - 1) + min;
        }
        public static double InExp(double t, double totaltime, double max, double min)
        {
            max -= min;
            return t == 0.0 ? min : max * Math.Pow(2, 10 * (t / totaltime - 1)) + min;
        }
        public static double OutExp(double t, double totaltime, double max, double min)
        {
            max -= min;
            return t == totaltime ? max + min : max * (-Math.Pow(2, -10 * t / totaltime) + 1) + min;
        }
        public static double InOutExp(double t, double totaltime, double max, double min)
        {
            if (t == 0.0)
                return min;
            if (t == totaltime)
                return max;
            max -= min;
            t /= totaltime;

            if (t / 2 < 1)
                return max / 2 * Math.Pow(2, 10 * (t - 1)) + min;
            --t;
            return max / 2 * (-Math.Pow(2, -10 * t) + 2) + min;

        }
        public static double InCirc(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            return -max * (Math.Sqrt(1 - t * t) - 1) + min;
        }
        public static double OutCirc(double t, double totaltime, double max, double min)
        {
            max -= min;
            t = t / totaltime - 1;
            return max * Math.Sqrt(1 - t * t) + min;
        }
        public static double InOutCirc(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;
            if (t / 2 < 1)
                return -max / 2 * (Math.Sqrt(1 - t * t) - 1) + min;
            t -= 2;
            return max / 2 * (Math.Sqrt(1 - t * t) + 1) + min;
        }
        public static double InBack(double t, double totaltime, double max, double min, double s)
        {
            max -= min;
            t /= totaltime;
            return max * t * t * ((s + 1) * t - s) + min;
        }
        public static double OutBack(double t, double totaltime, double max, double min, double s)
        {
            max -= min;
            t = t / totaltime - 1;
            return max * (t * t * ((s + 1) * t * s) + 1) + min;
        }
        public static double InOutBack(double t, double totaltime, double max, double min, double s)
        {
            max -= min;
            s *= 1.525;
            if (t / 2 < 1)
            {
                return max * (t * t * ((s + 1) * t - s)) + min;
            }
            t -= 2;
            return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
        }
        public static double OutBounce(double t, double totaltime, double max, double min)
        {
            max -= min;
            t /= totaltime;

            if (t < 1 / 2.75)
                return max * (7.5625 * t * t) + min;
            else if (t < 2 / 2.75)
            {
                t -= 1.5 / 2.75;
                return max * (7.5625 * t * t + 0.75) + min;
            }
            else if (t < 2.5 / 2.75)
            {
                t -= 2.25 / 2.75;
                return max * (7.5625 * t * t + 0.9375) + min;
            }
            else
            {
                t -= 2.625 / 2.75;
                return max * (7.5625 * t * t + 0.984375) + min;
            }
        }
        public static double InBounce(double t, double totaltime, double max, double min)
        {
            return max - OutBounce(totaltime - t, totaltime, max - min, 0) + min;
        }
        public static double InOutBounce(double t, double totaltime, double max, double min)
        {
            if (t < totaltime / 2)
                return InBounce(t * 2, totaltime, max - min, max) * 0.5 + min;
            else
                return OutBounce(t * 2 - totaltime, totaltime, max - min, 0) * 0.5 + min + (max - min) * 0.5;
        }
        public static double Linear(double t, double totaltime, double max, double min)
        {
            return (max - min) * t / totaltime + min;
        }
    }
}

