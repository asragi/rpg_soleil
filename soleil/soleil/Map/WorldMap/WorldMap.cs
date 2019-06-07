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
            SetPlayerPosition(WorldPointKey.Flare);

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

                SetEdge(WorldPointKey.Flare, WorldPointKey.Magistol, 2, result);
                SetEdge(WorldPointKey.Flare, WorldPointKey.Parel, 2, result);
                SetEdge(WorldPointKey.Flare, WorldPointKey.Earthband, 6, result);
                SetEdge(WorldPointKey.Somnia, WorldPointKey.Magistol, 4, result);
                SetEdge(WorldPointKey.Somnia, WorldPointKey.Shimaki, 8, result);
                SetEdge(WorldPointKey.Magistol, WorldPointKey.Parel, 3, result);
                SetEdge(WorldPointKey.Parel, WorldPointKey.AisenBerz, 7, result);
                return result;

                void Set(WorldPointKey key, Vector pos, WorldPoint[] array)
                    => array[(int)key] = new WorldPoint(key, pos);

                void SetEdge(WorldPointKey a, WorldPointKey b, int cost, WorldPoint[] array)
                {
                    array[(int)a].SetEdge(b, cost);
                    array[(int)b].SetEdge(a, cost);
                }
            }
        }

        public void SetPlayerPosition(WorldPointKey position)
        {
            foreach (var item in points)
            {
                if (item == null) continue;
                item.IsPlayerIn = item.ID == position;
            }
        }

        public WorldPoint GetPlayerPoint()
        {
            foreach (var item in points)
            {
                if (item == null) continue;
                if (item.IsPlayerIn) return item;
            }
            return null;
        }

        public void Draw(Drawing d)
        {
            points.ForEach2(p => p?.Draw(d));
        }
    }
}
