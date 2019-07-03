using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapMaster
    {
        public WorldMapMode Mode { get; private set; }
        WorldMap worldMap;
        WorldMapWindowLayer windowLayer;
        WorldMapInput mapInput;
        WorldMapCursorLayer cursorLayer;
        WorldMapSelectLayer mapSelectLayer;
        WorldMapCamera camera;
        WorldMapMove mapMove;
        WorldMapTransition mapTransition;
        BoxManager boxManager;

        public WorldMapMaster(WorldPointKey initialKey, WorldMapScene scene)
        {
            Mode = WorldMapMode.InitWindow;
            boxManager = new BoxManager();
            worldMap = new WorldMap(initialKey, boxManager);
            camera = new WorldMapCamera(scene.Camera);
            camera.SetPosition(worldMap.GetPoint(initialKey).Pos, true);
            mapMove = new WorldMapMove(worldMap, camera);
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
            cursorLayer = new WorldMapCursorLayer(camera, boxManager);
            mapSelectLayer = new WorldMapSelectLayer(worldMap, camera);
            mapTransition = new WorldMapTransition(scene);
            mapInput = new WorldMapInput(windowLayer, cursorLayer, mapSelectLayer, mapMove, worldMap, mapTransition);
        }

        public void Update()
        {
            Mode = mapInput.Update(Mode);
            Mode = mapMove.Update(Mode, windowLayer);
            cursorLayer.Update();
            camera.Update();
            worldMap.Update();
            mapTransition.Update(Mode);
            boxManager.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            cursorLayer.Draw(d);
            boxManager.Draw(d);
        }
    }
}
