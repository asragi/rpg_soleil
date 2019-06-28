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
            if (duration < frame) return;
            if (duration == frame)
            {
                camera.SetPositon(targetPos);
                return;
            }
            var X = Easing.OutQuart(frame, duration, targetPos.X, startPos.X);
            var Y = Easing.OutQuart(frame, duration, targetPos.Y, startPos.Y);
            var destination = new Vector(X, Y);
            camera.SetPositon(destination);
        }

        int frame = 10000000;
        const int duration = 60;
        Vector startPos;
        Vector targetPos;

        /// <summary>
        /// イージングによる滑らかな指定座標へのカメラ移動を開始する．
        /// </summary>
        public void SetDestination(WorldPoint point) => SetDestination(point.Pos);

        /// <summary>
        /// イージングによる滑らかな指定座標へのカメラ移動を開始する．
        /// </summary>
        public void SetDestination(Vector dest)
        {
            frame = 0;
            startPos = camera.GetPosition();
            targetPos = dest - CameraDiff;
        }

        /// <summary>
        /// 強制的にカメラの位置を設定する．
        /// </summary>
        public void SetPosition(Vector position)
        {
            camera.SetPositon(position - CameraDiff);
        }
    }
}
