using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    enum WorldMapInputMode
    {
        InitWindow,
        MapCursor,
        MapSelect,
        Menu,
    }

    class WorldMapInput
    {
        WorldMapInputMode mode;
        WorldMapWindowLayer windowLayer;

        public WorldMapInput(WorldMapWindowLayer wmwl)
        {
            mode = WorldMapInputMode.InitWindow;
            windowLayer = wmwl;
        }

        public void Update()
        {
            InputWindowLayer();

            void InputWindowLayer()
            {
                if (mode != WorldMapInputMode.InitWindow) return;
                if (KeyInput.GetKeyPush(Key.Up))
                {
                    windowLayer.UpCursor();
                }
                if (KeyInput.GetKeyPush(Key.Down))
                {
                    windowLayer.DownCursor();
                }
                if (KeyInput.GetKeyPush(Key.A))
                {
                    windowLayer.Decide();
                }
                var index = windowLayer.GetIndex();
                if (index == -1) return;
                windowLayer.QuitWindow();
                if (index == 0)
                {
                    // 移動先選択
                    mode = WorldMapInputMode.MapSelect;
                }
                if (index == 1)
                {
                    // マップ探索
                    mode = WorldMapInputMode.MapCursor;
                }
                if (index == 2)
                {
                    // 町・施設に入る
                }
            }
        }
    }
}
