using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMapWindowLayer
    {
        public static readonly Vector Position = new Vector(200, 170);
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
            initWindow = new SelectableWindow(Position, true, options);
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
