using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum ObjectDir { None = -1, R, DR, D, DL, L, UL, U, UR }

    static partial class MapDirection
    {
        public static int GetAngle(this ObjectDir dir)
        {
            if (dir == ObjectDir.None) throw new ArgumentOutOfRangeException();
            return (int)dir * 45;
        }
    }
}
