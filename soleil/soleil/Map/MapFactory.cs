using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map
{
    static class MapFactory
    {
        public static MapBase GetMap(MapName mapName, PersonParty party, Camera cam)
        {
            return SwitchMap(mapName, party, cam);
        }

        private static MapBase SwitchMap(MapName mapName, PersonParty party, Camera cam)
        {
            switch (mapName)
            {
                case MapName.Flare1:
                    return new Flare1(party, cam);
                case MapName.Somnia1:
                    return new Somnia1(party, cam);
                case MapName.Somnia2:
                    return new Somnia2(party, cam);
                case MapName.Somnia4:
                    return new Somnia4(party, cam);
                case MapName.MagistolRoom:
                    return new MagistolRoom(party, cam);
                case MapName.MagistolCol1:
                    return new MagistolCol1(party, cam);
                case MapName.MagistolShop:
                    return new MagistolShop(party, cam);
                case MapName.MagistolCol3:
                    return new MagistolCol3(party, cam);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
