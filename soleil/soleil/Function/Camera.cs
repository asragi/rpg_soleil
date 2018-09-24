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

        private Vector position;
        public Vector delta;
        Drawing drawing;

        public void SetDrawing(Drawing d)
        {
            drawing = d;
        }

        public void SetPositon(Vector pos)
        {
            position = pos;
        }

        public Vector GetPosition() => position;

        public void Update()
        {
            drawing.Camera = position + delta;
        }
    }
}
