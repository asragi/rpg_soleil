using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil {
    public enum InputFocus { None, Player, Window, }
    /// <summary>
    /// MapSceneでのInputの受付を管理するクラス
    /// </summary>
    class MapInputManager
    {
        InputFocus nowFocus;
        PlayerObject player;
        WindowManager wm;

        private static MapInputManager mapInputManager = new MapInputManager();
        public static MapInputManager GetInstance() => mapInputManager;
        private MapInputManager()
        {
            wm = WindowManager.GetInstance();
            nowFocus = InputFocus.Player;
        }
        public void SetPlayer(PlayerObject p) => player = p;

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
            if (KeyInput.GetKeyDown(Key.A)) player.Run();
            else player.Walk();

            /*
            if (KeyInput.GetKeyDown(Key.Left)) player.Move(new Vector(-1, 0));
            if (KeyInput.GetKeyDown(Key.Right)) player.Move(new Vector(1, 0));
            if (KeyInput.GetKeyDown(Key.Up)) player.Move(new Vector(0, -1));
            if (KeyInput.GetKeyDown(Key.Down)) player.Move(new Vector(0, 1));
            */

            player.Move(InputDirection());
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
