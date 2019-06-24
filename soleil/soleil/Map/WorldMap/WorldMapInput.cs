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
        public WorldMapInputMode Mode { get; set; }
        WorldMapWindowLayer windowLayer;
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer selectLayer;
        InputSmoother inputSmoother;

        public WorldMapInput(WorldMapWindowLayer wmwl, WorldMapCursorLayer cursor, WorldMapSelectLayer select)
        {
            Mode = WorldMapInputMode.InitWindow;
            windowLayer = wmwl;
            cursorLayer = cursor;
            selectLayer = select;
            inputSmoother = new InputSmoother();
        }

        public void Update()
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = inputSmoother.SmoothInput(inputDir);

            if (Mode == WorldMapInputMode.InitWindow) InputWindowLayer(smoothInput);
            else if (Mode == WorldMapInputMode.MapCursor) InputCursor(inputDir);
            else if (Mode == WorldMapInputMode.MapSelect) InputSelect(smoothInput);
            else if (Mode == WorldMapInputMode.Event) EventInput(smoothInput);

            // 最初に表示される「移動」「街に入る」などの選択を行うウィンドウ
            void InputWindowLayer(Direction dir)
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
                if (index == -1) return; // 選択肢未決定ならindexに-1が返される．
                windowLayer.QuitWindow();
                if (index == 0)
                {
                    // 移動先選択
                    Mode = WorldMapInputMode.MapSelect;
                    selectLayer.InitWindow();
                }
                if (index == 1)
                {
                    // マップ探索
                    Mode = WorldMapInputMode.MapCursor;
                    cursorLayer.Init();
                }
                if (index == 2)
                {
                    // 町・施設に入る
                }
            }
            // カーソルを自由に移動させて地図を眺めるモード．
            void InputCursor(Direction dir)
            {
                cursorLayer.Move(dir);
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    Mode = WorldMapInputMode.InitWindow;
                    cursorLayer.Quit();
                }
            }
            // 隣接する街から移動先を選ぶモード．
            void InputSelect(Direction dir)
            {
                selectLayer.MoveInput(dir);
                if (KeyInput.GetKeyPush(Key.A))
                {
                    var (movable, destination) = selectLayer.DecideDestination();
                    if (!movable)
                    {
                        // 時間がなくて移動ができないなどと表示．
                        return;
                    }
                    Mode = WorldMapInputMode.Move;
                    Console.WriteLine(destination);
                }
                if (KeyInput.GetKeyPush(Key.B))
                {
                    windowLayer.InitWindow();
                    Mode = WorldMapInputMode.InitWindow;
                    selectLayer.QuitWindow();
                }
            }

            // Event時の入力を受け取るモード．
            void EventInput(Direction dir)
            {

            }
        }
    }
}
