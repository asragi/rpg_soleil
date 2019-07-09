namespace Soleil.Map
{
    class MapManager
    {
        private static MapManager mapManager = new MapManager();
        public static MapManager GetInstance() => mapManager;

        MapBase nowMap;
        MapBase previousMap;

        public void ChangeMap(MapBase map, Vector position)
        {
            previousMap = nowMap;
            nowMap = map;
            nowMap.SetPlayerPos(position);
        }

        public void Update()
        {
            // 移動前マップでイベント処理が終わっていない場合、続行する。
            previousMap?.EventUpdate();
            nowMap.Update();

        }

        public void Draw(Drawing d)
        {
            nowMap.Draw(d);
        }
    }
}
