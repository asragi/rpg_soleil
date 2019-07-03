using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapSelectLayer
    {
        WorldPointKey[] keyList;
        int[] costList;
        SelectableWindow selectableWindow;
        WorldMap worldMap;
        WorldMapCamera camera;

        public WorldMapSelectLayer(WorldMap world, WorldMapCamera cam)
        {
            worldMap = world;
            camera = cam;
        }

        public void InitWindow()
        {
            var initialPosition = worldMap.GetPlayerPoint();
            var dict = initialPosition.Edges;
            var optionsList = new string[dict.Count];
            costList = new int[dict.Count];
            keyList = new WorldPointKey[dict.Count];
            int i = 0;
            foreach (var kvp in dict)
            {
                optionsList[i] = kvp.Key.ToString();
                keyList[i] = kvp.Key;
                costList[i] = kvp.Value;
                i++;
            }
            var pos = WorldMapWindowLayer.Position;
            selectableWindow = new SelectableWindow(pos, true, optionsList);

            selectableWindow.Call();
            RefreshDestination();
        }

        public void MoveInput(Direction dir)
        {
            if (dir == Direction.U)
            {
                selectableWindow.UpCursor();
                RefreshDestination();
            }
            if (dir == Direction.D)
            {
                selectableWindow.DownCursor();
                RefreshDestination();
            }
        }

        private void RefreshDestination()
        {
            var _key = keyList[selectableWindow.Index];
            camera.SetDestination(worldMap.GetPoint(_key));
        }

        public void QuitWindow()
        {
            selectableWindow.Quit();
            camera.SetDestination(worldMap.GetPlayerPoint());
        }

        /// <summary>
        /// 目的地を決定した際の処理．距離や時間帯によっては移動ができない．
        /// </summary>
        /// <returns>移動の可否と移動先を返す．</returns>
        public (bool, WorldPointKey) DecideDestination()
        {
            WorldPointKey destination = keyList[selectableWindow.Index];
            QuitWindow();
            return (true, destination);
        }
    }
}
