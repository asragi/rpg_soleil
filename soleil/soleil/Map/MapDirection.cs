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
    }
}
