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
            imageManager.Create(TextureID.FrameTest, Vector.Zero, DepthID.Debug);
        }

        override public void Update()
        {
            imageManager.Update();
            if (KeyInput.GetKeyPush(Key.C))
            {
                imageManager.MoveTo(0, new Vector(300, 300), 60, Easing.OutQuart);
                imageManager.FadeOut(0,30, Easing.OutCirc);
            }
            if (KeyInput.GetKeyPush(Key.D))
            {
                imageManager.MoveTo(0, new Vector(0, 0), 60, Easing.OutQuart);
                imageManager.FadeIn(0, 30, Easing.OutCirc);
            }
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
