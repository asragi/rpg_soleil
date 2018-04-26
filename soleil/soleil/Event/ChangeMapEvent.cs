namespace Soleil.Event
{
    /// <summary>
    /// マップを変更しプレイヤーの位置を変更する.
    /// </summary>
    class ChangeMapEvent : EventBase
    {
        MapManager mm;
        MapName nextMap;
        ObjectDir direction;
        Vector nextPos;


        /// <param name="_pos">移動先のマップ上での位置</param>
        /// <param name="dir">移動後のプレイヤーの向き</param>
        public ChangeMapEvent(MapName next, Vector _pos,ObjectDir dir)
            : base()
        {
            mm = MapManager.GetInstance();
            nextMap = next;
            nextPos = _pos;
            direction = dir;

        }

        public override void Execute()
        {
            var map = MapFactory.GetMap(nextMap);
            // マップを変更する
            mm.ChangeMap(map);
            // プレイヤーのポジションを変更する
            map.SetPlayerPos(nextPos);
            // プレイヤーの向きを変更する
            // player.SetDirection(direction);

            // 次のイベントへ
            Next();
        }
    }
}
