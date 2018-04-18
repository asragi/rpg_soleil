namespace Soleil
{
    class TestScene : Scene
    {
        TestMap testMap;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            testMap = new TestMap(wm);
        }

        override public void Update()
        {
            testMap.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            testMap.Draw(sb);
            base.Draw(sb);
        }
    }
}
