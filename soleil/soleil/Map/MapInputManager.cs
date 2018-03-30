using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil {
    public enum MapFocus { None, Player, Window, }
    /// <summary>
    /// MapSceneでのInputの受付を管理するクラス
    /// </summary>
    class MapInputManager
    {
        MapFocus nowFocus;
        PlayerObject player;
        WindowManager wm;

        public MapInputManager(PlayerObject p)
        {
            wm = WindowManager.GetInstance();
            player = p;
            nowFocus = MapFocus.Player;
        }

        public void Update()
        {
            switch (nowFocus)
            {
                case MapFocus.None:
                    break;
                case MapFocus.Player:
                    PlayerMove();
                    break;
                case MapFocus.Window:
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

        public void SetFocus(MapFocus f)
        {
            nowFocus = f;
        }

        private void PlayerMove()
        {
            if (KeyInput.GetKeyDown(Key.A)) player.Run();
            else player.Walk();
            if (KeyInput.GetKeyDown(Key.Left)) player.Move(new Vector(-1, 0));
            if (KeyInput.GetKeyDown(Key.Right)) player.Move(new Vector(1, 0));
            if (KeyInput.GetKeyDown(Key.Up)) player.Move(new Vector(0, -1));
            if (KeyInput.GetKeyDown(Key.Down)) player.Move(new Vector(0, 1));
        }
    }
}
