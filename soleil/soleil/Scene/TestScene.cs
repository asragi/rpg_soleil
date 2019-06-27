using Soleil.Map;

namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;
        MapIndicator mapIndicator;
        public TestScene(SceneManager sm, PersonParty party, MapName map, Vector position)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.ChangeMap(MapFactory.GetMap(map, party), position);
            mapIndicator = new MapIndicator();
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
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
