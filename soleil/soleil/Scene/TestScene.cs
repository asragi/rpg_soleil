using Soleil.Map;

namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;
        MapIndicator mapIndicator;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.ChangeMap(MapFactory.GetMap(MapName.Flare1));
            mapIndicator = new MapIndicator();
        }

        override public void Update()
        {
            mapIndicator.Update();
            mapManager.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            mapIndicator.Draw(sb);
            mapManager.Draw(sb);
            base.Draw(sb);
        }
    }
}
