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
        WorldMap worldMap;
        public WorldMapScene(SceneManager sm)
            : base(sm)
        {
            worldMap = new WorldMap();
        }

        public override void Draw(Drawing sb)
        {
            base.Draw(sb);
            worldMap.Draw(sb);
        }
    }
}
