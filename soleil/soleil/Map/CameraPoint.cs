using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    class CameraPoint
    {
        Vector pos;
        public CameraPoint(int x, int y)
        {
            pos = new Vector(x,y);
        }
        public CameraPoint(Vector vec)
        {
            pos = vec;
        }

        public Vector GetPos() => pos;
    }
}
