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
        WorldMapInput mapInput;

        public WorldMapMaster()
        {
            worldMap = new WorldMap();
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
            mapInput = new WorldMapInput(windowLayer);
        }

        public void Update()
        {
            mapInput.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
        }
    }
}
