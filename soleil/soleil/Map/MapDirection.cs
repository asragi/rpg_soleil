using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    enum ObjectDir {R=0, DR=1, D=2, DL=3, L=4, UL=5, U=6, UR=7,None }

    static partial class MapDirection
    {
        public static int GetAngle(this ObjectDir dir) => (int)dir * 45;
        public static int AnimNum(this ObjectDir dir)
        {
            switch (dir)
            {
                case ObjectDir.D:
                    return 4;
                case ObjectDir.DR:
                case ObjectDir.DL:
                    return 3;
                case ObjectDir.R:
                case ObjectDir.L:
                    return 2;
                case ObjectDir.UR:
                case ObjectDir.UL:
                    return 1;
                case ObjectDir.U:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static bool IsContainUp(this ObjectDir dir)
        {
            return (dir == ObjectDir.U) || (dir == ObjectDir.UL) || (dir == ObjectDir.UR);
        }
        public static bool IsContainDown(this ObjectDir dir)
        {
            return (dir == ObjectDir.D) || (dir == ObjectDir.DL) || (dir == ObjectDir.DR);
        }
    }
}
