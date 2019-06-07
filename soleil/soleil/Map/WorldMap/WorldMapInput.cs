using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    enum WorldMapInputMode
    {

    }

    class WorldMapInput
    {
        WorldMapWindowLayer windowLayer;

        public WorldMapInput(WorldMapWindowLayer wmwl)
        {
            windowLayer = wmwl;
        }

        public void Update()
        {
            if (KeyInput.GetKeyPush(Key.Up))
            {
                windowLayer.UpCursor();
            }
            if (KeyInput.GetKeyPush(Key.Down))
            {
                windowLayer.DownCursor();
            }
        }
    }
}
