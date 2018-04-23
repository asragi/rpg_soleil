namespace Soleil
{
    class MapManager
    {
        private static MapManager mapManager = new MapManager();
        public static MapManager GetInstance() => mapManager;

        Map nowMap;
        Map previousMap;
        PlayerObject player;

        public void ChangeMap(Map map)
        {
            previousMap = nowMap;
            nowMap = map;
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
