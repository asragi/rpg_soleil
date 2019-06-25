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
        Move,
        Event,
    }

    /// <summary>
    /// WolrdMapでInputを一元管理するクラス．
    /// </summary>
    class WorldMapInput
    {
        WorldMap worldMap;
        WorldMapWindowLayer windowLayer;
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer selectLayer;
        WorldMapMove mapMove;
        InputSmoother inputSmoother;

        public WorldMapInput(
            WorldMapWindowLayer wmwl, WorldMapCursorLayer cursor,
            WorldMapSelectLayer select, WorldMapMove move,
            WorldMap map)
        {
            windowLayer = wmwl;
            cursorLayer = cursor;
            selectLayer = select;
            mapMove = move;
            worldMap = map;
            inputSmoother = new InputSmoother();
        }

        public WorldMapInputMode Update(WorldMapInputMode mode)
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = inputSmoother.SmoothInput(inputDir);

            if (mode == WorldMapInputMode.InitWindow) return InputWindowLayer(smoothInput);
            else if (mode == WorldMapInputMode.MapCursor) return InputCursor(inputDir);
            else if (mode == WorldMapInputMode.MapSelect) return InputSelect(smoothInput);
            else if (mode == WorldMapInputMode.Event) return EventInput(smoothInput);
            return mode;

            // 最初に表示される「移動」「街に入る」などの選択を行うウィンドウ
            WorldMapInputMode InputWindowLayer(Direction dir)
            {
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
                if (index == -1) return WorldMapInputMode.InitWindow; // 選択肢未決定ならindexに-1が返される．
                windowLayer.QuitWindow();
                if (index == 0)
                {
                    // 移動先選択
                    selectLayer.InitWindow();
                    return WorldMapInputMode.MapSelect;
                }
                if (index == 1)
                {
                    // マップ探索
                    cursorLayer.Init();
                    return WorldMapInputMode.MapCursor;
                }
                if (index == 2)
                {
                    // 町・施設に入る
                }
                return WorldMapInputMode.InitWindow;
            }
            // カーソルを自由に移動させて地図を眺めるモード．
            WorldMapInputMode InputCursor(Direction dir)
            {
                cursorLayer.Move(dir);
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    cursorLayer.Quit();
                    return WorldMapInputMode.InitWindow;
                }
                return WorldMapInputMode.MapCursor;
            }
            // 隣接する街から移動先を選ぶモード．
            WorldMapInputMode InputSelect(Direction dir)
            {
                selectLayer.MoveInput(dir);
                if (KeyInput.GetKeyPush(Key.A))
                {
                    var (movable, destination) = selectLayer.DecideDestination();
                    if (!movable)
                    {
                        // 時間がなくて移動ができないなどと表示．
                        return WorldMapInputMode.MapSelect;
                    }
                    mapMove.MoveFromTo(worldMap.GetPlayerPoint(), worldMap.GetPoint(destination));
                    return WorldMapInputMode.Move;
                }
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    selectLayer.QuitWindow();
                    return WorldMapInputMode.InitWindow;
                }
                return WorldMapInputMode.MapSelect;
            }

            // Event時の入力を受け取るモード．
            WorldMapInputMode EventInput(Direction dir)
            {
                return WorldMapInputMode.Event;
            }
        }
    }
}
