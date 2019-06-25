using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapCamera
    {
        static Vector CameraDiff = new Vector(Game1.VirtualCenterX, Game1.VirtualCenterY);
        Camera camera;
        public WorldMapCamera()
        {
            camera = Camera.GeInstance();
        }

        public void Update()
        {
            camera.Update();

            EasingCamera();
        }

        private void EasingCamera()
        {
            frame++;
            if (duration <= frame)
            {
                camera.SetPositon(targetPos);
                return;
            }
            var X = Easing.OutQuart(frame, duration, targetPos.X, startPos.X);
            var Y = Easing.OutQuart(frame, duration, targetPos.Y, startPos.Y);
            var destination = new Vector(X, Y);
            camera.SetPositon(destination);
        }

        int frame = 0;
        const int duration = 60;
        Vector startPos;
        Vector targetPos;
        public void SetDestination(WorldPoint point) => SetDestination(point.Pos);

        public void SetDestination(Vector dest)
        {
            frame = 0;
            startPos = camera.GetPosition();
            targetPos = dest - CameraDiff;
        }
    }
}
