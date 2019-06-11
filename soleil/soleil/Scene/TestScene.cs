using Soleil.Map;

namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;
        MapIndicator mapIndicator;
        public TestScene(SceneManager sm, PersonParty party)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.Party = party;
            mapManager.ChangeMap(MapFactory.GetMap(MapName.Somnia4, party));
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
