using Soleil.Map;
using Soleil.Map.WorldMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class WorldMapScene: Scene
    {
        WorldMapMaster worldMapMaster;
        PersonParty party;

        public WorldMapScene(SceneManager sm, PersonParty _party, WorldPointKey pointKey)
            : base(sm)
        {
            worldMapMaster = new WorldMapMaster(pointKey, this);
            party = _party;
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
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            worldMapMaster.Draw(sb);
        }
    }
}
