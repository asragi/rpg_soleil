using Soleil.Map;

namespace Soleil
{
    class TestScene : Scene
    {
        //Map testMap;
        MapManager mapManager;
        MapIndicator mapIndicator;
        ImageManager imageManager;
        public TestScene(SceneManager sm)
            : base(sm)
        {
            mapManager = MapManager.GetInstance();
            mapManager.ChangeMap(MapFactory.GetMap(MapName.Somnia1));
            mapIndicator = new MapIndicator();
            imageManager = new ImageManager();
            imageManager.Create(TextureID.FrameTest, Vector.Zero, DepthID.Debug);
        }

        override public void Update()
        {
            // 動作確認用 debug
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
            // 動作確認用終わり
            mapIndicator.Update();
            imageManager.Update();
            mapManager.Update();
            base.Update();
        }

        override public void Draw(Drawing sb)
        {
            mapIndicator.Draw(sb);
            imageManager.Draw(sb);
            mapManager.Draw(sb);
            base.Draw(sb);
        }
    }
}
