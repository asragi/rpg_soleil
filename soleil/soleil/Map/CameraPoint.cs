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
        public CameraPoint(Vector _pos)
        {
            pos = _pos;
        }

        public Vector GetPos() => pos;
    }
}
