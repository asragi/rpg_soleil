using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    class Camera
    {
        public Vector Position;
        public Vector Delta;

        public void Move(Drawing d)
        {
            d.Camera = Position + Delta;
        }
    }
}
