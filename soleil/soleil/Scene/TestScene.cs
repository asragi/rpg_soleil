using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            mapManager.ChangeMap(MapFactory.GetMap(map, party, Camera), position);
            mapIndicator = new MapIndicator();
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);

            // test
            var save = new Misc.SaveData(party);
            string path = "./test_save_data";
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, save);
            }
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
