using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapSelectLayer
    {
        readonly string TimeUnit = "時間";
        WorldPointKey[] keyList;
        int[] costList;
        SelectableWindow selectableWindow;
        MessageWindow messageWindow;
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
            messageWindow = new MessageWindow(pos + new Vector(350, 0), MessageWindow.GetProperSize(MessageWindow.DefaultFont, "n" + TimeUnit), WindowTag.A, WindowManager.GetInstance(), true);
            selectableWindow.Call();
            messageWindow.Call();
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
            var _index = selectableWindow.Index;
            SetMessage(_index);
            var key = keyList[_index];
            camera.SetDestination(worldMap.GetPoint(key));

            void SetMessage(int index)
            {
                if (selectableWindow == null) return;
                messageWindow.Text = costList[index].ToString() + TimeUnit;
            }
        }

        public void QuitWindow()
        {
            selectableWindow.Quit();
            messageWindow.Quit();
        }
    }
}
