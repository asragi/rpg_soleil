using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    class Camera
    {
        // Singleton
        private static Camera camera = new Camera();
        private Camera(){}
        public static Camera GeInstance() => camera;

        public Vector Position;
        public Vector Delta;
        Drawing drawing;

        public void SetDrawing(Drawing d)
        {
            drawing = d;
        }

        public void Move(Drawing d)
        {
            d.Camera = Position + Delta;
        }
    }
}
