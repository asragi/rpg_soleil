using Soleil.Map;

namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;
        ImageManager imageManager;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.ChangeMap(MapFactory.GetMap(MapName.Somnia1));

            imageManager = new ImageManager();
        }

        override public void Update()
        {
            imageManager.Update();
            mapManager.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            imageManager.Draw(sb);
            mapManager.Draw(sb);
            base.Draw(sb);
        }
    }
}
