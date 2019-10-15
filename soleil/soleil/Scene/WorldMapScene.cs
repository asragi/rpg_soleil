using Soleil.Map;
using Soleil.Map.WorldMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class WorldMapScene : Scene
    {
        WorldMapMaster worldMapMaster;
        PersonParty party;
        MapIndicator mapIndicator;

        public WorldMapScene(SceneManager sm, PersonParty _party, WorldPointKey pointKey)
            : base(sm)
        {
            worldMapMaster = new WorldMapMaster(pointKey, this, _party);
            party = _party;
            var transition = Transition.GetInstance();
            transition.SetMode(TransitionMode.FadeIn);
            mapIndicator = new MapIndicator();
        }

        public void ChangeSceneToMap(WorldPointKey key)
        {
            MapName name;
            Vector position;
            (name, position) = WorldMapPositionData.Get(key);
            new TestScene(SceneManager, party, name, position);
            Kill();
        }

        public override void Update()
        {
            base.Update();
            worldMapMaster.Update();
            mapIndicator.Update();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            worldMapMaster.Draw(sb);
            mapIndicator.Draw(sb);
        }
    }
}
