using Soleil.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    public enum InputFocus { None, Player, Window, Menu,}
    /// <summary>
    /// MapSceneでのInputの受付を管理するクラス
    /// </summary>
    class MapInputManager
    {
        Dictionary<Key, bool> inputs;
        InputFocus nowFocus;
        PlayerObject player;
        WindowManager wm;
        MenuSystem menuSystem;

        private static MapInputManager mapInputManager = new MapInputManager();
        public static MapInputManager GetInstance() => mapInputManager;
        private MapInputManager()
        {
            wm = WindowManager.GetInstance();
            menuSystem = new MenuSystem();
            nowFocus = InputFocus.Player;
            inputs = new Dictionary<Key, bool>();
        }
        public void SetPlayer(PlayerObject p) => player = p;
        public void SetMenuSystem(MenuSystem m) => menuSystem = m; // 地獄

        public void Update()
        {
            // 入力を受け取る
            var inputDir = InputDirection();
            UpdateInputKeysDown();
            // フォーカスに応じて処理を振り分ける
            switch (nowFocus)
            {
                case InputFocus.Player:
                    PlayerMove(inputDir);
                    break;
                case InputFocus.Window:
                    SelectWindowMove(inputDir);
                    break;
                case InputFocus.Menu:
                    menuSystem.Input(inputDir);
                    // debug
                    if (menuSystem.IsQuit) nowFocus = InputFocus.Player;
                    break;
                default:
                    break;
            }
        }

        private void SelectWindowMove(ObjectDir inputDir)
        {
            if (inputDir.IsContainUp()) wm.MoveCursor(Key.Up);
            else if (inputDir.IsContainDown()) wm.MoveCursor(Key.Down);
            if (inputs[Key.A]) wm.Decide();
        }

        public void SetFocus(InputFocus f)
        {
            nowFocus = f;
        }

        private void PlayerMove(ObjectDir inputDir)
        {
            // Run, Dash or stand
            if (inputs[Key.A]) player.Run();
            else player.Walk();
            if (inputDir == ObjectDir.None) player.Stand();

            // Call Menu
            if (inputs[Key.B])
            {
                menuSystem.CallMenu();
                nowFocus = InputFocus.Menu;
                return;
            }

            player.Move(inputDir);
        }

        /// <summary>
        /// 入力に応じて8方向のEnumを返す
        /// </summary>
        ObjectDir InputDirection()
        {
            if (KeyInput.GetKeyDown(Key.Right))
            {
                if (KeyInput.GetKeyDown(Key.Up))
                {
                    return ObjectDir.UR;
                }

                if (KeyInput.GetKeyDown(Key.Down))
                {
                    return ObjectDir.DR;
                }
                return ObjectDir.R;
            }
            if (KeyInput.GetKeyDown(Key.Left))
            {
                if (KeyInput.GetKeyDown(Key.Up))
                {
                    return ObjectDir.UL;
                }

                if (KeyInput.GetKeyDown(Key.Down))
                {
                    return ObjectDir.DL;
                }
                return ObjectDir.L;
            }
            if (KeyInput.GetKeyDown(Key.Up))
            {
                return ObjectDir.U;
            }

            if (KeyInput.GetKeyDown(Key.Down))
            {
                return ObjectDir.D;
            }
            return ObjectDir.None;
        }

        void UpdateInputKeysDown()
        {
            inputs[Key.A] = KeyInput.GetKeyDown(Key.A);
            inputs[Key.B] = KeyInput.GetKeyDown(Key.B);
            inputs[Key.C] = KeyInput.GetKeyDown(Key.C);
            inputs[Key.D] = KeyInput.GetKeyDown(Key.D);
        }
    }
}
