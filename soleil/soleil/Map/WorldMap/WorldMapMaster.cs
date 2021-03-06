﻿using Soleil.Menu;
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
        MenuSystem menuSystem;

        public WorldMapMaster(WorldPointKey initialKey, WorldMapScene scene, PersonParty _party)
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
            menuSystem = new MenuSystem(_party);
            mapInput = new WorldMapInput(windowLayer, cursorLayer, mapSelectLayer, mapMove, worldMap, mapTransition, menuSystem);
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
            menuSystem.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            cursorLayer.Draw(d);
            boxManager.Draw(d);
            menuSystem.Draw(d);
        }
    }
}
