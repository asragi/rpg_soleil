using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    class Camera
    {
        private Vector position;
        public Vector delta { get; set; }

        public void SetPositon(Vector pos) => position = pos;
        public Vector GetPosition() => position + delta;
    }
}
