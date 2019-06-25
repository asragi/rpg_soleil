using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapMaster
    {
        public WorldMapInputMode Mode { get; private set; }
        WorldMap worldMap;
        WorldMapWindowLayer windowLayer;
        WorldMapInput mapInput;
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer mapSelectLayer;
        WorldMapCamera camera;
        WorldMapMove mapMove;

        public WorldMapMaster()
        {
            Mode = WorldMapInputMode.InitWindow;
            worldMap = new WorldMap();
            mapMove = new WorldMapMove(worldMap);
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
            cursorLayer = new WorldMapCursorLayer();
            camera = new WorldMapCamera();
            mapSelectLayer = new WorldMapSelectLayer(worldMap, camera);
            mapInput = new WorldMapInput(windowLayer, cursorLayer, mapSelectLayer, mapMove, worldMap);
        }

        public void Update()
        {
            Mode = mapInput.Update(Mode);
            Mode = mapMove.Update(Mode, windowLayer);
            cursorLayer.Update();
            camera.Update();
            Console.WriteLine(Mode);
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            cursorLayer.Draw(d);
        }
    }
}
