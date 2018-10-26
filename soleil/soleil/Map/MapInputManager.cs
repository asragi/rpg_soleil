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
            nowFocus = InputFocus.Player;
            inputs = new Dictionary<Key, bool>();
        }
        public void SetPlayer(PlayerObject p) => player = p;
        public void SetMenuSystem(MenuSystem m) => menuSystem = m; // 地獄

        // 入力を良い感じにする処理用
        const int InputWait = 6;
        int waitFrame;
        const int HeadWait = 20;
        int headWaitCount;
        bool inputCheck;

        public void Update()
        {
            // 入力を受け取る
            var inputDir = KeyInput.GetStickInclineDirection(1);
            var smoothInput = InputSmoother(inputDir);
            UpdateInputKeysDown();
            // フォーカスに応じて処理を振り分ける
            switch (nowFocus)
            {
                case InputFocus.Player:
                    PlayerMove();
                    break;
                case InputFocus.Window:
                    wm.Input(smoothInput);
                    break;
                case InputFocus.Menu:
                    menuSystem.Input(smoothInput);
                    // debug
                    if (menuSystem.IsQuit) nowFocus = InputFocus.Player;
                    break;
                default:
                    break;
            }
        }

        public void SetFocus(InputFocus f)
        {
            nowFocus = f;
        }

        private void PlayerMove()
        {
            var inputDir = KeyInput.GetStickInclineDirection(1);
            if (KeyInput.GetKeyPush(Key.A))
            {
                player.ProjectHitBox();
            }
            // Run, Dash or stand
            if (KeyInput.GetKeyDown(Key.A))
            {
                player.Run();
            }
            else player.Walk();
            if (inputDir == Direction.N) player.Stand();

            // Call Menu
            if (KeyInput.GetKeyPush(Key.B))
            {
                menuSystem.CallMenu();
                nowFocus = InputFocus.Menu;
                return;
            }

            player.Move(inputDir);
        }

        /// <summary>
        /// 入力押しっぱなしでも毎フレーム移動しないようにする関数
        /// </summary>
        private Direction InputSmoother(Direction dir)
        {
            waitFrame--;
            if (dir != Direction.N)
            {
                headWaitCount--;
                if (headWaitCount > 0 && inputCheck) return Direction.N;
                if (waitFrame > 0) return Direction.N;
                waitFrame = InputWait;
                inputCheck = true;
                return dir;
            }
            inputCheck = false;
            waitFrame = 0;
            headWaitCount = HeadWait;
            return Direction.N;
        }

        void UpdateInputKeysDown()
        {
            inputs[Key.A] = KeyInput.GetKeyPush(Key.A);
            inputs[Key.B] = KeyInput.GetKeyPush(Key.B);
            inputs[Key.C] = KeyInput.GetKeyPush(Key.C);
            inputs[Key.D] = KeyInput.GetKeyPush(Key.D);
        }
    }
}
