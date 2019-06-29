using Soleil.Map;

namespace Soleil.Event
{
    /// <summary>
    /// マップを変更しプレイヤーの位置を変更する.
    /// </summary>
    class ChangeMapEvent : EventBase
    {
        MapManager mm;
        MapName nextMap;
        Direction direction;
        Vector nextPos;
        PersonParty party;

        /// <param name="_pos">移動先のマップ上での位置</param>
        /// <param name="dir">移動後のプレイヤーの向き</param>
        public ChangeMapEvent(MapName next, Vector _pos, Direction dir, PersonParty _party)
            : base()
        {
            mm = MapManager.GetInstance();
            nextMap = next;
            nextPos = _pos;
            direction = dir;
            party = _party;
        }

        public override void Execute()
        {
            var map = MapFactory.GetMap(nextMap, party);
            // マップを変更する
            mm.ChangeMap(map, nextPos);
            // プレイヤーの向きを変更する
            map.SetPlayerDir(direction);

            // 次のイベントへ
            Next();
        }
    }
}
