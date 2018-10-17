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
                case MapName.Flare1:
                    return new Flare1();
                case MapName.Somnia1:
                    return new Somnia1();
                case MapName.Somnia2:
                    return new Somnia2();
                case MapName.Somnia4:
                    return new Somnia4();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
