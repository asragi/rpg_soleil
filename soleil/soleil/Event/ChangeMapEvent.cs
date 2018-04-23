using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Event
{
    /// <summary>
    /// マップを変更しプレイヤーの位置を変更する.
    /// </summary>
    class ChangeMapEvent : EventBase
    {
        MapManager mm;
        MapName nextMap;
        PlayerMoveDir direction;
        PlayerObject player;
        Vector nextPos;


        /// <param name="_pos">移動先のマップ上での位置</param>
        /// <param name="dir">移動後のプレイヤーの向き</param>
        public ChangeMapEvent(PlayerObject _player,MapName next, Vector _pos,PlayerMoveDir dir)
            : base()
        {
            mm = MapManager.GetInstance();
            nextMap = next;
            nextPos = _pos;
            player = _player;
            direction = dir;

        }

        public override void Execute()
        {
            // マップを変更する
            mm.ChangeMap(new TestMap2(WindowManager.GetInstance()));
            // プレイヤーのポジションを変更する
            player.SetPosition(nextPos);
            // プレイヤーの向きを変更する
            // player.SetDirection(direction);

            // 次のイベントへ
            Next();
        }
    }
}
