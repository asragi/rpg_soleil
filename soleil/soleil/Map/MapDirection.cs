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
    }
}
