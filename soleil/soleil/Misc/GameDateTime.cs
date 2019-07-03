using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Misc
{
    enum Weekday
    {
        Sunday = 0,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sanday
    }

    /// <summary>
    /// ゲーム中の時間を表すクラス
    /// </summary>
    class GameDateTime
    {
        /// <summary>
        /// ゲーム開始時からの経過Minutes合計: 実質的なカウンタ．
        /// </summary>
        public int MinutesSum { get; private set; }
        /// <summary>
        /// ゲーム開始からの経過Hoursの合計．
        /// </summary>
        public int HoursSum => MinutesSum / 60;
        /// <summary>
        /// 24h表示での現在時刻．
        /// </summary>
        public int NowHour => HoursSum % 24;
        public int NowMinutes => MinutesSum % 60;
        /// <summary>
        /// 1-Origin.
        /// </summary>
        public int NowDay => HoursSum / 24 + 1;
        public Weekday NowWeekDay => (Weekday)(NowDay % 7);
        /// <summary>
        /// 昼判定を返す．6-17時: true
        /// </summary>
        bool IsDaytime => (6 <= NowHour && NowHour <= 17);

        public GameDateTime(int day, int hour, int minutes)
        {
            if (day < 1) throw new ArgumentOutOfRangeException("day should be over 0");
            MinutesSum = (day - 1) * 24 * 60 + hour * 60 + minutes;
        }

        /// <summary>
        /// 時間を経過させる．
        /// </summary>
        public void Pass(int day = 0, int hour = 0, int minute = 0)
        {
            MinutesSum += day * 24 * 60 + hour * 60 + minute;
        }
    }
}
