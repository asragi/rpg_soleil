using Soleil.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    enum WorldMapMode
    {
        InitWindow,
        MapCursor,
        MapSelect,
        Menu,
        Move,
        Event,
        Transition,
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
        WorldMapTransition mapTransition;

        public WorldMapInput(
            WorldMapWindowLayer wmwl, WorldMapCursorLayer cursor,
            WorldMapSelectLayer select, WorldMapMove move,
            WorldMap map, WorldMapTransition transition)
        {
            windowLayer = wmwl;
            cursorLayer = cursor;
            selectLayer = select;
            mapMove = move;
            worldMap = map;
            mapTransition = transition;
            inputSmoother = new InputSmoother();
        }

        public WorldMapMode Update(WorldMapMode mode)
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = inputSmoother.SmoothInput(inputDir);

            if (mode == WorldMapMode.InitWindow) return InputWindowLayer(smoothInput);
            else if (mode == WorldMapMode.MapCursor) return InputCursor(inputDir);
            else if (mode == WorldMapMode.MapSelect) return InputSelect(smoothInput);
            else if (mode == WorldMapMode.Event) return EventInput(smoothInput);
            return mode;

            // 最初に表示される「移動」「街に入る」などの選択を行うウィンドウ
            WorldMapMode InputWindowLayer(Direction dir)
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
                if (index == -1) return WorldMapMode.InitWindow; // 選択肢未決定ならindexに-1が返される．
                windowLayer.QuitWindow();
                if (index == 0)
                {
                    // 移動先選択
                    selectLayer.InitWindow();
                    return WorldMapMode.MapSelect;
                }
                if (index == 1)
                {
                    // マップ探索
                    cursorLayer.Init(worldMap.GetPlayerPoint().Pos);
                    return WorldMapMode.MapCursor;
                }
                if (index == 2)
                {
                    // 町・施設に入る
                    mapTransition.Init(worldMap.GetPlayerPoint().ID);
                    return WorldMapMode.Transition;
                }
                return WorldMapMode.InitWindow;
            }
            // カーソルを自由に移動させて地図を眺めるモード．
            WorldMapMode InputCursor(Direction dir)
            {
                cursorLayer.Move(dir);
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    cursorLayer.Quit(worldMap.GetPlayerPoint().Pos);
                    return WorldMapMode.InitWindow;
                }
                return WorldMapMode.MapCursor;
            }
            // 隣接する街から移動先を選ぶモード．
            WorldMapMode InputSelect(Direction dir)
            {
                selectLayer.MoveInput(dir);
                if (KeyInput.GetKeyPush(Key.A))
                {
                    var (movable, destination) = selectLayer.DecideDestination();
                    if (!movable)
                    {
                        // 時間がなくて移動ができないなどと表示．
                        return WorldMapMode.MapSelect;
                    }
                    mapMove.MoveFromTo(worldMap.GetPlayerPoint(), worldMap.GetPoint(destination));
                    return WorldMapMode.Move;
                }
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    selectLayer.QuitWindow();
                    return WorldMapMode.InitWindow;
                }
                return WorldMapMode.MapSelect;
            }

            // Event時の入力を受け取るモード．
            WorldMapMode EventInput(Direction dir)
            {
                return WorldMapMode.Event;
            }
        }
    }
}
