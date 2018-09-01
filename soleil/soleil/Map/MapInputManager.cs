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
        }
        public void SetPlayer(PlayerObject p) => player = p;
        public void SetMenuSystem(MenuSystem m) => menuSystem = m; // 地獄

        public void Update()
        {
            switch (nowFocus)
            {
                case InputFocus.None:
                    break;
                case InputFocus.Player:
                    PlayerMove();
                    break;
                case InputFocus.Window:
                    SelectWindowMove();
                    break;
                case InputFocus.Menu:
                    var input = InputDirection();
                    menuSystem.MoveCursor(input);
                    // debug
                    if (menuSystem.IsQuit) nowFocus = InputFocus.Player;
                    break;
                default:
                    break;
            }
        }

        private void SelectWindowMove()
        {
            if (KeyInput.GetKeyPush(Key.Up)) wm.MoveCursor(Key.Up);
            else if (KeyInput.GetKeyPush(Key.Down)) wm.MoveCursor(Key.Down);
            if (KeyInput.GetKeyPush(Key.A)) wm.Decide();
        }

        public void SetFocus(InputFocus f)
        {
            nowFocus = f;
        }

        private void PlayerMove()
        {
            var inputDir = InputDirection();

            
            // Run, Dash or stand
            if (KeyInput.GetKeyDown(Key.A)) player.Run();
            else player.Walk();
            if (inputDir == ObjectDir.None) player.Stand();

            // Call Menu
            if (KeyInput.GetKeyDown(Key.B))
            {
                menuSystem.CallMenu();
                nowFocus = InputFocus.Menu;
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
    }
}
