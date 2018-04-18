namespace Soleil
{
    class TestScene : Scene
    {
        TestMap testMap;
        Transition transition; // debug
        public TestScene(SceneManager sm)
            : base(sm)
        {
            testMap = new TestMap(wm);
            transition = new Transition();
        }

        override public void Update()
        {
            if (KeyInput.GetKeyPush(Key.B)) transition.SetMode(TransitionMode.FadeIn);
            if (KeyInput.GetKeyPush(Key.A)) transition.SetMode(TransitionMode.FadeOut);
            testMap.Update();
            transition.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            transition.Draw(sb);
            testMap.Draw(sb);
            base.Draw(sb);
        }
    }
}
