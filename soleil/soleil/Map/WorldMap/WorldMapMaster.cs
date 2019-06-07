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
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer mapSelectLayer;
        WorldMapCamera camera;

        public WorldMapMaster()
        {
            worldMap = new WorldMap();
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
            cursorLayer = new WorldMapCursorLayer();
            mapSelectLayer = new WorldMapSelectLayer(worldMap);
            mapInput = new WorldMapInput(windowLayer, cursorLayer, mapSelectLayer);
            camera = new WorldMapCamera();
        }

        public void Update()
        {
            mapInput.Update();
            cursorLayer.Update();
            mapSelectLayer.Update();
            camera.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            cursorLayer.Draw(d);
        }
    }
}
