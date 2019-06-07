using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapSelectLayer
    {
        int[] costList;
        SelectableWindow selectableWindow;
        MessageWindow messageWindow;
        WorldMap worldMap;

        public WorldMapSelectLayer(WorldMap world) { worldMap = world; }

        public void InitWindow()
        {
            var initialPosition = worldMap.GetPlayerPoint();
            var dict = initialPosition.Edges;
            var optionsList = new string[dict.Count];
            costList = new int[dict.Count];
            int i = 0;
            foreach (var kvp in dict)
            {
                optionsList[i] = kvp.Key.ToString();
                costList[i] = kvp.Value;
                i++;
            }
            selectableWindow = new SelectableWindow(Vector.Zero, true, optionsList);
            messageWindow = new MessageWindow(Vector.Zero, new Vector(200, 100), WindowTag.A, WindowManager.GetInstance());
            selectableWindow.Call();
            messageWindow.Call();
        }

        public void QuitWindow()
        {
            selectableWindow.Quit();
            messageWindow.Quit();
        }
    }
}
