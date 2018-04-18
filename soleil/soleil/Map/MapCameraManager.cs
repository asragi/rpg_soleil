using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
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
        public MapCameraManager(PlayerObject _player)
        {
            player = _player;
            camera = Camera.GeInstance();
        }

        public void SetMapSize(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;
            maxX = mapWidth - Game1.VirtualWindowSizeX;
            maxY = mapHeight - Game1.VirtualWindowSizeY;
        }

        public void Update()
        {
            camera.Update();
            AdjustCamera();
        }

        private void AdjustCamera()
        {
            // debug
            var pos = player.GetPosition() - new Vector(Game1.VirtualWindowSizeX/2,Game1.VirtualWindowSizeY/2);
            pos = ClampCameraPos(pos);
            camera.SetPositon(pos);
        }

        private Vector ClampCameraPos(Vector pos)
        {
            Vector temp;
            temp.X = Math.Min(maxX, Math.Max(0, pos.X));
            temp.Y = Math.Min(maxY, Math.Max(0, pos.Y));
            return temp;
        }
    }
}
