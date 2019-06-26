﻿using Soleil.Map.WorldMap;
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
        public WorldMapScene(SceneManager sm)
            : base(sm)
        {
            worldMapMaster = new WorldMapMaster();
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