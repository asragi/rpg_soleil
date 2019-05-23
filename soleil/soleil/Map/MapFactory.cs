using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    static class MapFactory
    {
        public static MapBase GetMap(MapName mapName, PersonParty party)
        {
            return SwitchMap(mapName, party);
        }

        private static MapBase SwitchMap(MapName mapName, PersonParty party)
        {
            switch (mapName)
            {
                case MapName.Flare1:
                    return new Flare1(party);
                case MapName.Somnia1:
                    return new Somnia1(party);
                case MapName.Somnia2:
                    return new Somnia2(party);
                case MapName.Somnia4:
                    return new Somnia4(party);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
