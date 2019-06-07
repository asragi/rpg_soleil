using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapCamera
    {
        Camera camera;
        public WorldMapCamera()
        {
            camera = Camera.GeInstance();
        }

        int i;
        public void Update()
        {
            camera.Update();
            i++;
            camera.SetPositon(new Vector(Math.Sin(i), 0));
        }
    }
}
