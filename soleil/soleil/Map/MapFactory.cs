using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    static class MapFactory
    {
        public static MapBase GetMap(MapName mapName)
        {
            return SwitchMap(mapName);
        }

        private static MapBase SwitchMap(MapName mapName)
        {
            switch (mapName)
            {
                case MapName.Somnia1:
                    return new Somnia1();
                case MapName.Somnia2:
                    return new TestMap();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
