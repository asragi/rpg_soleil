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
        MessageWindow messageWindow, descriptionWindow;
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
            messageWindow = new MessageWindow(pos + new Vector(350, 100), MessageWindow.GetProperSize(MessageWindow.DefaultFont, "n" + TimeUnit), WindowTag.A, WindowManager.GetInstance(), true);
            descriptionWindow = new MessageWindow(pos + new Vector(350, 0), MessageWindow.GetProperSize(MessageWindow.DefaultFont, WorldPoint.Descriptions[WorldPointKey.Magistol]), WindowTag.A, WindowManager.GetInstance(), true);
            selectableWindow.Call();
            descriptionWindow.Call();
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
            var _key = keyList[_index];
            SetMessage(_index, _key);
            camera.SetDestination(worldMap.GetPoint(_key));

            void SetMessage(int index, WorldPointKey key)
            {
                if (selectableWindow == null) return;
                messageWindow.Text = costList[index].ToString() + TimeUnit;
                descriptionWindow.Text = WorldPoint.Descriptions[key];
            }
        }

        public void QuitWindow()
        {
            selectableWindow.Quit();
            messageWindow.Quit();
            descriptionWindow.Quit();
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
