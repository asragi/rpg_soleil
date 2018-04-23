using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    static class MapFactory
    {
        public static Map GetMap(MapName mapName)
        {
            return SwitchMap(mapName);
        }

        private static Map SwitchMap(MapName mapName)
        {
            switch (mapName)
            {
                case MapName.Somnia1:
                    return new TestMap2();
                case MapName.Somnia2:
                    return new TestMap();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
