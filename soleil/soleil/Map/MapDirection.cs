using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    static partial class MapDirection
    {
        public static bool IsContainUp(this Direction dir)
        {
            return (dir == Direction.U) || (dir == Direction.LU) || (dir == Direction.RU);
        }
        public static bool IsContainDown(this Direction dir)
        {
            return (dir == Direction.D) || (dir == Direction.LD) || (dir == Direction.RD);
        }
        public static int Angle(this Direction dir)
        {
            return ((int)dir - 1) * 45;
        }

        public static Direction ToDirection(this Vector vec)
        {
            var angle = vec.GetAngle();
            // 角度の基準をDirectionに合わせる．
            angle = 180 + angle;
            // 角度中心から左右に判定を広げる．
            angle = (angle + 22.5) % 360;
            return (Direction)(angle / 45 + 1);
        }
    }
}
