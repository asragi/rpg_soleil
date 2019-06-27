﻿using System;
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

        public WorldMapMaster(WorldPointKey initialKey)
        {
            Mode = WorldMapInputMode.InitWindow;
            worldMap = new WorldMap(initialKey);
            camera = new WorldMapCamera();
            camera.SetPosition(worldMap.GetPoint(initialKey).Pos);
            mapMove = new WorldMapMove(worldMap, camera);
            windowLayer = new WorldMapWindowLayer();
            windowLayer.InitWindow();
            cursorLayer = new WorldMapCursorLayer();
            mapSelectLayer = new WorldMapSelectLayer(worldMap, camera);
            mapInput = new WorldMapInput(windowLayer, cursorLayer, mapSelectLayer, mapMove, worldMap);
        }

        public void Update()
        {
            Mode = mapInput.Update(Mode);
            Mode = mapMove.Update(Mode, windowLayer);
            cursorLayer.Update();
            camera.Update();
            worldMap.Update();
        }

        public void Draw(Drawing d)
        {
            worldMap.Draw(d);
            cursorLayer.Draw(d);
        }
    }
}
