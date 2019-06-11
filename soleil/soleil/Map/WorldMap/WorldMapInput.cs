using Soleil.Misc;
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
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer selectLayer;
        InputSmoother inputSmoother;

        public WorldMapInput(WorldMapWindowLayer wmwl, WorldMapCursorLayer cursor, WorldMapSelectLayer select)
        {
            mode = WorldMapInputMode.InitWindow;
            windowLayer = wmwl;
            cursorLayer = cursor;
            selectLayer = select;
            inputSmoother = new InputSmoother();
        }

        public void Update()
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = inputSmoother.SmoothInput(inputDir);
            InputWindowLayer(smoothInput);
            InputCursor(inputDir);
            InputSelect(smoothInput);

            void InputWindowLayer(Direction dir)
            {
                if (mode != WorldMapInputMode.InitWindow) return;
                if (dir == Direction.U)
                {
                    windowLayer.UpCursor();
                }
                if (dir == Direction.D)
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
                    selectLayer.InitWindow();
                }
                if (index == 1)
                {
                    // マップ探索
                    mode = WorldMapInputMode.MapCursor;
                    cursorLayer.Init();
                }
                if (index == 2)
                {
                    // 町・施設に入る
                }
            }

            void InputCursor(Direction dir)
            {
                if (mode != WorldMapInputMode.MapCursor) return;
                cursorLayer.Move(dir);
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    mode = WorldMapInputMode.InitWindow;
                    cursorLayer.Quit();
                }
            }

            void InputSelect(Direction dir)
            {
                if (mode != WorldMapInputMode.MapSelect) return;
                selectLayer.MoveInput(dir);
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    mode = WorldMapInputMode.InitWindow;
                    selectLayer.QuitWindow();
                }
            }
        }
    }
}
