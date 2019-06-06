using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapWindowLayer
    {
        WindowManager wm;
        public WorldMapWindowLayer()
        {
            wm = WindowManager.GetInstance();
        }

        public void InitWindow()
        {
            var options = new[]
            {
                "移動",
                "マップ",
                "施設に入る"
            };
            var selectWindow = new SelectableWindow(Vector.Zero, options);
        }

        public void Update()
        {
            wm.Update();
        }

        public void Draw(Drawing d)
        {
            wm.Draw(d);
        }
    }
}
