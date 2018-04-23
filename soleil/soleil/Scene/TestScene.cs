namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;

        public TestScene(SceneManager sm)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.AddNew(new TestMap(wm));
        }

        override public void Update()
        {
            mapManager.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            mapManager.Draw(sb);
            base.Draw(sb);
        }
    }
}
