using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapWindowLayer
    {
        string[] options = new[]
            {
                "移動",
                "マップ",
                "施設に入る"
            };
        SelectableWindow initWindow;

        public WorldMapWindowLayer() { }

        public void InitWindow()
        {
            initWindow = new SelectableWindow(Vector.Zero, true, options);
            initWindow.Call();
            initWindow.Reset();
        }

        public void QuitWindow()
        {
            initWindow.Quit();
        }

        public int GetIndex() => initWindow.ReturnIndex();
        public void Decide() => initWindow.Decide();
        public void UpCursor() => initWindow.UpCursor();
        public void DownCursor() => initWindow.DownCursor();
    }
}
