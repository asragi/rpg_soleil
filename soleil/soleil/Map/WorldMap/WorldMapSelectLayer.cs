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
            var pos = WorldMapWindowLayer.Position;
            selectableWindow = new SelectableWindow(pos, true, optionsList);
            messageWindow = new MessageWindow(pos + new Vector(350, 0), MessageWindow.GetProperSize(MessageWindow.DefaultFont, "n" + TimeUnit), WindowTag.A, WindowManager.GetInstance());
            selectableWindow.Call();
            messageWindow.Call();
        }

        public void MoveInput(Direction dir)
        {
            if (dir == Direction.U) selectableWindow.UpCursor();
            if (dir == Direction.D) selectableWindow.DownCursor();
        }

        public void Update()
        {
            SetMessage();
            void SetMessage()
            {
                if (selectableWindow == null) return;
                var index = selectableWindow.Index;
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
