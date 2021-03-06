﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    /// <summary>
    /// Map毎に生成するCameraを管理するクラス
    /// </summary>
    class MapCameraManager
    {
        int mapWidth, mapHeight;
        int maxX, maxY;
        PlayerObject player;
        Camera camera;
        CameraPoint[] cameraPoints;
        static Vector CameraDiff = new Vector(Game1.VirtualCenterX, Game1.VirtualCenterY);

        public MapCameraManager(PlayerObject _player, Camera cam)
        {
            player = _player;
            camera = cam;
        }

        public void SetCameraPoint(CameraPoint[] point) => cameraPoints = point;

        public void SetMapSize(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;
            maxX = mapWidth - Game1.VirtualCenterX;
            maxY = mapHeight - Game1.VirtualCenterY;
        }

        public void Update()
        {
            AdjustCamera();
        }

        private void AdjustCamera()
        {
            // debug
            // var tempPos = player.GetPosition() - new Vector(Game1.VirtualWindowSizeX/2,Game1.VirtualWindowSizeY/2);
            // tempPos = ClampCameraPos(tempPos);
            Vector tempPos;
            tempPos = SmoothMove() - CameraDiff;
            camera.SetPositon(tempPos);
        }

        #region CameraStrategy

        int nowTarget = -1;
        int duration = 60;
        int frame = 0;
        Vector startPos;
        Vector targetPos;
        Vector SmoothMove()
        {
            double minLength = 10000;
            int nextTarget = 0;
            for (int i = 0; i < cameraPoints.Length; i++)
            {
                var distance = (cameraPoints[i].GetPos() - player.GetPosition()).GetLength();
                if (distance < minLength)
                {
                    minLength = distance;
                    nextTarget = i;
                }
            }
            if (nextTarget != nowTarget)
            {
                startPos = (nowTarget == -1) ? ClampCameraPos(player.GetPosition()) : camera.GetPosition() + CameraDiff;
                nowTarget = nextTarget;
                targetPos = ClampCameraPos(cameraPoints[nextTarget].GetPos());
                frame = 0;
            }
            var X = Easing.OutQuart(frame, duration, targetPos.X, startPos.X);
            var Y = Easing.OutQuart(frame, duration, targetPos.Y, startPos.Y);
            if (frame < duration) frame++;
            return new Vector(X, Y);
        }

        #endregion




        private Vector ClampCameraPos(Vector pos)
        {
            Vector temp;
            temp.X = Math.Min(maxX, Math.Max(Game1.VirtualCenterX, pos.X));
            temp.Y = Math.Min(maxY, Math.Max(Game1.VirtualCenterY, pos.Y));
            return temp;
        }
    }
}
