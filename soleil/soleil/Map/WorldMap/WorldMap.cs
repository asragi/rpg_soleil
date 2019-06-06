using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMap
    {
        WorldPoint[] points;
        public WorldMap()
        {
            points = MakePoints();

            WorldPoint[] MakePoints()
            {
                var result = new WorldPoint[(int)WorldPointKey.size];
                Set(WorldPointKey.Flare, new Vector(400, 300), result);
                Set(WorldPointKey.Somnia, new Vector(800, 250), result);
                Set(WorldPointKey.Magistol, new Vector(450, 200), result);
                Set(WorldPointKey.Parel, new Vector(500, 320), result);
                Set(WorldPointKey.Shimaki, new Vector(800, 100), result);
                Set(WorldPointKey.Earthband, new Vector(100, 100), result);
                Set(WorldPointKey.AisenBerz, new Vector(850, 500), result);
                return result;

                void Set(WorldPointKey key, Vector pos, WorldPoint[] array)
                    => array[(int)key] = new WorldPoint(key, pos);
            }
        }

        public void Draw(Drawing d)
        {
            points.ForEach2(p => p?.Draw(d));
        }
    }
}
