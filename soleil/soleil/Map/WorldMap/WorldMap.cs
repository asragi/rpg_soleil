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
            var length = Enum.GetNames(typeof(WorldPointKey)).Length;
            points = new WorldPoint[length];
            points[0] = new WorldPoint(WorldPointKey.Somnia, new Vector(300, 300));
        }

        public void Draw(Drawing d)
        {
            points.ForEach2(p => p?.Draw(d));
        }
    }
}
