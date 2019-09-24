namespace Soleil.Map
{
    class MapManager
    {
        private static MapManager mapManager = new MapManager();
        public static MapManager GetInstance() => mapManager;

        public MapBase NowMap { get; private set; }
        MapBase previousMap;

        public void ChangeMap(MapBase map, Vector position)
        {
            previousMap = NowMap;
            NowMap = map;
            NowMap.SetPlayerPos(position);
        }

        public void Update()
        {
            // 移動前マップでイベント処理が終わっていない場合、続行する。
            previousMap?.EventUpdate();
            NowMap.Update();

        }

        public void Draw(Drawing d)
        {
            NowMap.Draw(d);
        }
    }
}
