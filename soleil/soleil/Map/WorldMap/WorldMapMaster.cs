using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapMaster
    {
        WorldMap worldMap;
        WorldMapWindowLayer windowLayer;

        public WorldMapMaster()
        {
            worldMap = new WorldMap();
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
        }

        public void Update()
        {
            windowLayer.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            windowLayer.Draw(d);
        }
    }
}
