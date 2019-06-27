using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    /// <summary>
    /// WorldMap上の地点のKeyから行先Mapと初期地点を得るための静的クラス
    /// </summary>
    static class WorldMapPositionData
    {
        static Dictionary<WorldPointKey, Vector> positionDict;
        static Dictionary<WorldPointKey, MapName> nameDict;

        static WorldMapPositionData()
        {
            positionDict = new Dictionary<WorldPointKey, Vector>();
            nameDict = new Dictionary<WorldPointKey, MapName>();
            Set(WorldPointKey.Flare, MapName.Flare1, new Vector(0, 0));
            Set(WorldPointKey.Somnia, MapName.Somnia1, new Vector(600, 600));

            void Set(WorldPointKey key, MapName name, Vector pos)
            {
                positionDict.Add(key, pos);
                nameDict.Add(key, name);
            }
        }

        public static (MapName, Vector) Get(WorldPointKey key)
            => (nameDict[key], positionDict[key]);
    }
}
